using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
/*
- Add a passenger/crewmember if they find that their relative is missing from the database. DONE

- Look up which city their relative deported from. DONE

- Look up which class their relative traveled with. DONE

- Check which of all the other passengers traveled in the same class as their relative. DONE 

    B. Alla era frågor ska vara i form av stored procedures, era frågor ska innehålla
    variabler (DONE), vilkor(DONE) och loopar(DONE-ISH)

    C. Vissa av era stored procedures ska ha in parametrar, och vissa av de ska ha
    möjlighet att inte behöva skicka med ett argument för en viss parameter. Så att
    ni inte tvingar användare att tillhanda värden för alla argument i en stored
    procedures. DONE

    D. Ni ska använda er av aggregat funktioner där det behövs för er business case. DONE

*/
public partial class StoredProcedures
{
    [SqlProcedure]
    public static SqlInt32 InsertPassenger(SqlString Lastname, SqlString Firstname, SqlString Age, SqlInt32 CabinID, SqlString Ticket, SqlString TicketPrice, SqlInt32 CityID, SqlString Job )
    {
        Passenger newPassenger = new Passenger(Firstname, Lastname, Age, CabinID, Ticket, TicketPrice, CityID, Job);

        if(newPassenger.CheckInputs())
            return 0;

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Passenger (Lastname, Firstname, Age, CabinID, Ticket, TicketPrice, CityID, Job) VALUES ('" + newPassenger.Lastname.ToString() + "', '" + newPassenger.Firstname.ToString() + "', '" + newPassenger.Age.ToString() + "'," + newPassenger.CabinID + ", '" + newPassenger.Ticket.ToString() + "', '" + newPassenger.TicketPrice.ToString() + "', " + CityID + ", '" + Job.ToString() + "') ";
            comm.Connection = conn;
            comm.Parameters.Add(new SqlParameter("test", SqlInt32.Null));
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            return 1;
        }
    }

    [SqlProcedure]
    public static SqlInt32 InsertCrew(SqlString Lastname, SqlString Firstname, SqlInt32 Age, SqlInt32 DepartmentID, SqlInt32 CityID, SqlString Job, SqlInt32 ClassID)
    {
        Crew newCrew = new Crew(Firstname, Lastname, Age, DepartmentID, CityID, Job, ClassID);

        if (newCrew.CheckInputs())
            return 0;        

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Crew (CrewID, Lastname, Firstname, Age, DepartmentID, CityID, Job, ClassID) VALUES (" + newCrew.CrewID + ",'" + newCrew.Lastname.ToString() + "', '" + newCrew.Firstname.ToString() + "', " + newCrew.Age + "," + newCrew.DepartmentID + ", " + newCrew.CityID + ", '" + newCrew.Job.ToString() + "', " + newCrew.ClassID + ") ";
            comm.Connection = conn;

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            return 1;
        }
    }

    [SqlProcedure]
    public static void GetAllInCrew()
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT Crew.CrewID" +
                              ",CONCAT(Crew.Firstname, ' '" +
                              ",Crew.Lastname) AS 'Full name'" +
                              ",Crew.Age" +
                              ",Department.DepartmentDescription" +
                              ",DepartCity.cityName" +
                              ",Crew.Job" +
                              ",Class.ClassDescription " +
                              "FROM Crew " +
                              "JOIN Department ON Crew.DepartmentID = Department.DepartmentID " +
                              "JOIN DepartCity ON Crew.CityID = DepartCity.cityID " +
                              "JOIN Class ON Crew.ClassID = Class.ClassID " +
                              "SELECT COUNT(*) as '# in Crew' FROM Crew";
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [SqlProcedure]
    public static void GetCrewFromCity( SqlInt32 CityID )
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT Crew.CrewID" +
                              ",CONCAT(Crew.Firstname, ' '" +
                              ",Crew.Lastname) AS 'Full name'" +
                              ",Crew.Age" +
                              ",Department.DepartmentDescription" +
                              ",DepartCity.cityName" +
                              ",Crew.Job" +
                              ",Class.ClassDescription " +
                              "FROM Crew " +
                              "JOIN Department ON Crew.DepartmentID = Department.DepartmentID " +
                              "JOIN DepartCity ON Crew.CityID = DepartCity.cityID " +
                              "JOIN Class ON Crew.ClassID = Class.ClassID " +
                              "WHERE Crew.CityID = " + CityID;
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [SqlProcedure]
    public static void GetPassengerFromCity(SqlInt32 CityID)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT CONCAT(Passenger.Firstname, ' '" +
                              ",Passenger.Lastname) AS 'Full name'" +
                              ",Passenger.Firstname" +
                              ",Passenger.Age" +
                              ",Cabin.CabinDescription" +
                              ",DepartCity.cityName" +
                              ",Passenger.Job" +
                              ",Class.ClassDescription " +
                              "FROM Passenger " +
                              "JOIN Cabin ON Cabin.CabinID = Passenger.CabinID " +
                              "JOIN DepartCity ON Passenger.CityID = DepartCity.cityID " +
                              "JOIN Class ON Cabin.ClassID = Class.ClassID " +
                              "WHERE Passenger.CityID = " + CityID;
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [SqlProcedure]
    public static void GetClassOfPassenger(SqlString Firstname, SqlString Lastname)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT CONCAT(Passenger.Firstname, ' '" +
                              ",Passenger.Lastname) AS 'Full name'" +
                              ",Passenger.Age" +
                              ",Cabin.CabinDescription" +
                              ",DepartCity.cityName" +
                              ",Class.ClassDescription " +
                              ",Class.ClassID " +
                              "FROM Passenger " +
                              "JOIN Cabin ON Cabin.CabinID = Passenger.CabinID " +
                              "JOIN DepartCity ON Passenger.CityID = DepartCity.cityID " +
                              "JOIN Class ON Cabin.ClassID = Class.ClassID " +
                              "WHERE Passenger.Firstname LIKE '%" + Firstname.ToString() + "%' AND Passenger.Lastname LIKE '%" + Lastname.ToString() + "%'";
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [SqlProcedure]
    public static void GetClassOfPassengers(SqlString Firstname, SqlString Lastname)
    {
        SqlConnection conn = new SqlConnection("Context Connection=true");
        conn.Open();
        SqlCommand getPassCommand = new SqlCommand("SELECT TOP(1) Passenger.Lastname" +
                              ",Passenger.Firstname" +
                              ",Passenger.Age" +
                              ",Cabin.CabinDescription" +
                              ",DepartCity.cityName" +
                              ",Class.ClassDescription " +
                              ",Class.ClassID " +
                              "FROM Passenger " +
                              "JOIN Cabin ON Cabin.CabinID = Passenger.CabinID " +
                              "JOIN DepartCity ON Passenger.CityID = DepartCity.cityID " +
                              "JOIN Class ON Cabin.ClassID = Class.ClassID " +
                              "WHERE Passenger.Firstname LIKE '%" + Firstname.ToString() + "%' AND Passenger.Lastname LIKE '%" + Lastname.ToString() + "%'", conn);

        SqlDataReader getPassengerRdr = getPassCommand.ExecuteReader();

        string _classID = "";

        while (getPassengerRdr.Read())
        {
             _classID = getPassengerRdr["ClassID"].ToString();
        }
        
        conn.Close();
        
        SqlCommand comm = new SqlCommand();
        comm.CommandText = "SELECT CONCAT(Passenger.Firstname, ' '" + 
                          ",Passenger.Lastname) AS 'Full name'" +
                          ",Passenger.Age" +
                          ",Cabin.CabinDescription" +
                          ",DepartCity.cityName" +
                          ",Class.ClassDescription " +
                          "FROM Passenger " +
                          "JOIN Cabin ON Cabin.CabinID = Passenger.CabinID " +
                          "JOIN DepartCity ON Passenger.CityID = DepartCity.cityID " +
                          "JOIN Class ON Cabin.ClassID = Class.ClassID " +
                          "WHERE Class.ClassID = " + SqlInt32.Parse(_classID);
        comm.Connection = conn;
        conn.Open();
        SqlContext.Pipe.ExecuteAndSend(comm);
        conn.Close();
        conn.Dispose();
        comm.Dispose();
    }
}
