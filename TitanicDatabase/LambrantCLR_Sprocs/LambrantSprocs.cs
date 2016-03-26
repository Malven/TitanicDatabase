using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

/*

- Search amongst all the passengers and crewmembers by age.

- Check what kind of cabin the relative lived in based on lastname.

- If their relative was a member of the crew they should be able to check which department he/she worked for.

- If their relative was a member of the crew they should be able to check which class they took care of, 1st-, 2nd- or 3rd class.

*/

public class LambrantSprocs
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static SqlInt32 SearchByAge(SqlString age)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            int tempAge;
            bool isNum = int.TryParse(age.ToString(), out tempAge);

            if (tempAge < 0 || tempAge > 120 || !isNum)
            {
                return 0;
            }
            else
            {
                comm.CommandText = "SELECT Lastname, Firstname, Age " +
                                    "FROM Passenger " +
                                    "WHERE Age = " + age.ToString() + " UNION ALL " +
                                    "SELECT Lastname, Firstname, Age " +
                                    "FROM Crew " +
                                    "WHERE Age = " + age.ToString() + ";";

                comm.Connection = conn;
                conn.Open();
                //comm.ExecuteNonQuery();
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
                conn.Dispose();
                comm.Dispose();
                return 1;
            }            
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CabinPassenger(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();
            

            comm.CommandText = "SELECT p.Lastname, p.Firstname, cab.CabinDescription, " +
                                "cab.CabinPrice " + 
                                "FROM Passenger AS p " +
                                "INNER JOIN Cabin AS cab ON cab.CabinID = p.CabinID " +
                                "WHERE p.Lastname = '" + name.ToString() + "';";

            comm.Connection = conn;
            conn.Open();
            //comm.ExecuteNonQuery();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CrewDepartment(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();


            comm.CommandText = "SELECT c.Lastname, c.Firstname, d.DepartmentDescription " +
                                "FROM Crew AS c " +
                                "INNER JOIN Department AS d ON d.DepartmentID = c.DepartmentID " +
                                "WHERE c.Lastname = '" + name.ToString() + "';";

            comm.Connection = conn;
            conn.Open();
            //comm.ExecuteNonQuery();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void CrewClass(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();


            comm.CommandText = "SELECT c.Lastname, c.Firstname, cl.ClassDescription AS WorkedFor " +
                                "FROM Crew AS c " + 
                                "INNER JOIN Class AS cl ON cl.ClassID = c.ClassID " +
                                "WHERE c.Lastname = '" + name.ToString() + "';";

            comm.Connection = conn;
            conn.Open();
            //comm.ExecuteNonQuery();
            SqlContext.Pipe.ExecuteAndSend(comm);
            conn.Close();
            conn.Dispose();
            comm.Dispose();
        }
    }
}
