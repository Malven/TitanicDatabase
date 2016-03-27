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
    [SqlProcedure]
    public static SqlInt32 SearchByAge(SqlString age)
    {
        
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            int temp;
            bool isNum = int.TryParse(age.ToString(), out temp);

            if (age.ToString() == "")
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(Lastname + ', ', '') + Firstname, Lastname) AS FullName, Age " +
                                    "FROM Passenger " +
                                    "UNION ALL " +
                                    "SELECT COALESCE(COALESCE(Lastname + ', ', '') + Firstname, Lastname) AS FullName, Age " +
                                    "FROM Crew ";

                comm.Connection = conn;
                conn.Open();
                //comm.ExecuteNonQuery();
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
                conn.Dispose();
                comm.Dispose();
                return 1;
            }
            else if (temp < 0 || temp > 120 || !isNum)
            {
                return 0;
            }
            else
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(Lastname + ', ', '') + Firstname, Lastname) AS FullName, Age " +
                                    "FROM Passenger " +
                                    "WHERE Age = " + age.ToString() + " UNION ALL " +
                                    "SELECT COALESCE(COALESCE(Lastname + ', ', '') + Firstname, Lastname) AS FullName, Age " +
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

    [SqlProcedure]
    public static SqlInt32 CabinPassenger(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            int temp;
            bool isNum = int.TryParse(name.ToString(), out temp);

            if (name.ToString() == "")
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(p.Lastname + ', ', '') + p.Firstname, p.Lastname) AS FullName, cab.CabinDescription AS Cabin " +
                                   "FROM Passenger AS p " +
                                   "INNER JOIN Cabin AS cab ON cab.CabinID = p.CabinID ";

                comm.Connection = conn;
                conn.Open();
                //comm.ExecuteNonQuery();
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
                conn.Dispose();
                comm.Dispose();
                return 1;
            }
            else if (name.ToString() == null || isNum)
            {
                return 0;
            }
            else
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(p.Lastname + ', ', '') + p.Firstname, p.Lastname) AS FullName, cab.CabinDescription, " +
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
                return 1;
            }            
        }
    }

    [SqlProcedure]
    public static SqlInt32 CrewDepartment(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            int temp;
            bool isNum = int.TryParse(name.ToString(), out temp);

            if (name.ToString() == "")
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, d.DepartmentDescription AS Department, c.Job " + 
                                   "FROM Crew AS c " +
                                   "INNER JOIN Department AS d ON d.DepartmentID = c.DepartmentID";

                comm.Connection = conn;
                conn.Open();
                //comm.ExecuteNonQuery();
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
                conn.Dispose();
                comm.Dispose();
                return 1;
            }
            else if (name.ToString() == null || isNum)
            {
                return 0;
            }
            else
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, d.DepartmentDescription AS Department, c.Job " +
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
                return 1;
            }
        }
    }

    [SqlProcedure]
    public static SqlInt32 CrewClass(SqlString name)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            int temp;
            bool isNum = int.TryParse(name.ToString(), out temp);

            if (name.ToString() == "")
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, cl.ClassDescription AS WorkedFor, c.Job AS WorkedAs " +
                                   "FROM Crew AS c " +
                                   "LEFT JOIN Class AS cl ON cl.ClassID = c.ClassID ";

                comm.Connection = conn;
                conn.Open();
                //comm.ExecuteNonQuery();
                SqlContext.Pipe.ExecuteAndSend(comm);
                conn.Close();
                conn.Dispose();
                comm.Dispose();
                return 1;
            }
            else if (name.ToString() == null || isNum)
            {
                return 0;
            }
            else
            {
                comm.CommandText = "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, cl.ClassDescription AS WorkedFor, c.Job AS WorkedAs " +
                                   "FROM Crew AS c " +
                                   "LEFT JOIN Class AS cl ON cl.ClassID = c.ClassID " +
                                   "WHERE c.Lastname = '" + name.ToString() + "' " +
                                   "AND c.ClassID IS NULL OR " +
                                   "c.Lastname = '" + name.ToString() + "' " +
                                   "AND c.ClassID IS NOT NULL;";

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
}
