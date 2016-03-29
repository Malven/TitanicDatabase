using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    /// <summary>
    /// Creates an instance of the Passenger class, CabinID and CityID have default values of 1
    /// </summary>
    /// <param name="Lastname"></param>
    /// <param name="Firstname"></param>
    /// <param name="Age"></param>
    /// <param name="CabinID"></param>
    /// <param name="Ticket"></param>
    /// <param name="TicketPrice"></param>
    /// <param name="CityID"></param>
    /// <param name="Job"></param>
    /// <returns>1 for success, 0 for failure</returns>
    [SqlProcedure]
    public static SqlInt32 InsertPassenger(SqlString Lastname, SqlString Firstname, SqlString Age, SqlInt32 CabinID, SqlString Ticket, SqlString TicketPrice, SqlInt32 CityID, SqlString Job)
    {
        if (CityID.IsNull)
            CityID = 1;
        if (CabinID.IsNull)
            CabinID = 1;

        Passenger newPassenger = new Passenger(Firstname, Lastname, Age, CabinID, Ticket, TicketPrice, CityID, Job);

        if(newPassenger.CheckInputs())
            return 0;

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Passenger (Lastname, Firstname, Age, CabinID, Ticket, TicketPrice, CityID, Job) VALUES ('" + newPassenger.Lastname.ToString() + "', '" + newPassenger.Firstname.ToString() + "', '" + newPassenger.Age.ToString() + "'," + newPassenger.CabinID + ", '" + newPassenger.Ticket.ToString() + "', '" + newPassenger.TicketPrice.ToString() + "', " + newPassenger.CityID + ", '" + newPassenger.Job.ToString() + "') ";
            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            return 1;
        }
    }

    /// <summary>
    /// Creates an instance of the Crew class, DepartmentID, ClassID and CityID have default values of 1
    /// </summary>
    /// <param name="Lastname"></param>
    /// <param name="Firstname"></param>
    /// <param name="Age"></param>
    /// <param name="DepartmentID"></param>
    /// <param name="CityID"></param>
    /// <param name="Job"></param>
    /// <param name="ClassID"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Selects all in Crew table
    /// </summary>
    /// <returns>1 for success</returns>
    [SqlProcedure]
    public static SqlInt32 GetAllInCrew()
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
            return 1;
        }
    }

    /// <summary>
    /// Select the crew from a specific city, if CityID is not applied all the crew will be selected
    /// </summary>
    /// <param name="CityID"></param>
    /// <returns>1 for success</returns>
    [SqlProcedure]
    public static SqlInt32 GetCrewFromCity( SqlInt32 CityID )
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            string query = "SELECT Crew.CrewID" +
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
                              "JOIN Class ON Crew.ClassID = Class.ClassID ";
            if (CityID != SqlInt32.Null || CityID <= 4)
                query += "WHERE Crew.CityID = " + CityID;

            comm.CommandText = query;
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            return 1;
        }
    }

    /// <summary>
    /// Select all passengers from a city, if CityID is not applied all passengers will be selected
    /// </summary>
    /// <param name="CityID">1-4</param>
    /// <returns>1 for success, anything else is failure</returns>
    [SqlProcedure]
    public static SqlInt32 GetPassengerFromCity(SqlInt32 CityID)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            string query = "SELECT CONCAT(Passenger.Firstname, ' '" +
                              ",Passenger.Lastname) AS 'Full name'" +
                              ",Passenger.Firstname" +
                              ",Passenger.Age" +
                              ",Cabin.CabinDescription" +
                              ",DepartCity.cityName" +
                              ",CASE WHEN Passenger.Job IS NULL THEN 'No job listed' ELSE Passenger.Job END AS 'Job'" +
                              ",Class.ClassDescription " +
                              "FROM Passenger " +
                              "JOIN Cabin ON Cabin.CabinID = Passenger.CabinID " +
                              "JOIN DepartCity ON Passenger.CityID = DepartCity.cityID " +
                              "JOIN Class ON Cabin.ClassID = Class.ClassID ";
            if(CityID != SqlInt32.Null || CityID <= 4)
                query += "WHERE Passenger.CityID = " + CityID;

            comm.CommandText = query;
            comm.Connection = conn;

            conn.Open();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
            return 1;
        }
    }

    /// <summary>
    /// Selects the Class of a Passenger
    /// </summary>
    /// <param name="Firstname"></param>
    /// <param name="Lastname"></param>
    /// <returns>1 for success</returns>
    [SqlProcedure]
    public static SqlInt32 GetClassOfPassenger(SqlString Firstname, SqlString Lastname)
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
            return 1;
        }
    }

    /// <summary>
    /// Selects passengers of the same class as another passenger
    /// </summary>
    /// <param name="Firstname"></param>
    /// <param name="Lastname"></param>
    /// <returns>1 for success</returns>
    [SqlProcedure]
    public static SqlInt32 GetClassOfPassengers(SqlString Firstname, SqlString Lastname)
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

        if (_classID == string.Empty)
            return 0;

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
        return 1;
    }
}
