using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
/*
- Add a passenger/crewmember if they find that their relative is missing from the database.

- Look up which city their relative deported from.

- Look up which class their relative traveled with.

- Check which of all the other passengers traveled in the same class as their relative.
*/
public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static SqlInt32 InsertPassenger(SqlString Lastname, SqlString Firstname, SqlString Age, SqlInt32? CabinID = null, SqlString? Ticket = null, SqlString? TicketPrice = null, SqlInt32? CityID = null, SqlString? Job = null)
    {
        if(!CheckInputs(Lastname, Firstname, Age))
            return 0;
        

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Passenger (Lastname, Firstname, Age, CabinID, Ticket, TicketPrice, CityID, Job) VALUES ('" + Lastname.ToString() + "', '" + Firstname.ToString() + "', '" + Age.ToString() + "'," + CabinID + ", '" + Ticket.ToString() + "', '" + TicketPrice.ToString() + "', " + CityID + ", '" + Job.ToString() + "') ";
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
    public static SqlInt32 InsertCrew(SqlInt32 CrewID, SqlString Lastname, SqlString Firstname, SqlInt32 Age, SqlInt32 DepartmentID, SqlInt32 CityID, SqlString Job, SqlInt32 ClassID)
    {

        if (!CheckInputs(Lastname, Firstname, Age.ToSqlString()))
            return 0;

        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Crew (CrewID, Lastname, Firstname, Age, DepartmentID, CityID, Job, ClassID) VALUES (" + CrewID + ",'" + Lastname.ToString() + "', '" + Firstname.ToString() + "', " + Age + "," + DepartmentID + ", " + CityID + ", '" + Job.ToString() + "', " + ClassID + ") ";
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
            comm.CommandText = "SELECT * FROM Crew";
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
                              ",Crew.Lastname" +
                              ",Crew.Firstname" +
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
            comm.CommandText = "SELECT Passenger.PassengerID" +
                              ",Passenger.Lastname" +
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

    private static bool CheckInputs(SqlString Lastname, SqlString Firstname, SqlString Age)
    {
        int tempAge;
        bool isNum = int.TryParse(Age.ToString(), out tempAge);

        if (tempAge < 0 || tempAge > 120 || !isNum)
            return false;

        if (Firstname == "" || Lastname == "")
            return false;

        return true;
    }
}
