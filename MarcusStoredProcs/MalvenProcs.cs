using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

public class MalvenProcs
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InsertPassenger(SqlString Lastname, SqlString Firstname, SqlString Age, SqlInt32 CabinID, SqlString Ticket, SqlString TicketPrice, SqlInt32 CityID, SqlString Job )
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Passenger (Lastname, Firstname, Age, CabinID, Ticket, TicketPrice, CityID, Job) VALUES ('"+ Lastname.ToString() +"', '"+ Firstname.ToString() +"', '"+ Age.ToString() +"',"+  CabinID +", '"+ Ticket.ToString() +"', '"+ TicketPrice.ToString() +"', "+ CityID +", '"+ Job.ToString() +"') ";
            comm.Connection = conn;

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void InsertCrew(SqlInt32 CrewID, SqlString Lastname, SqlString Firstname, SqlInt32 Age, SqlInt32 DepartmentID, SqlInt32 CityID, SqlString Job, SqlInt32 ClassID)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "INSERT INTO Crew (CrewID, Lastname, Firstname, Age, DepartmentID, CityID, Job, ClassID) VALUES (" + CrewID + ",'" + Lastname.ToString() + "', '" + Firstname.ToString() + "', " + Age + "," + DepartmentID + ", " + CityID + ", '" + Job.ToString() + "', "+ClassID+") ";
            comm.Connection = conn;

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
        }
    }
}
