using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;

public partial class StoredProcedures
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
            SqlString[] classNull = new SqlString[1104];

            if (name.ToString() == "")
            {
                //-----------everything from here to
                conn.Open();
                SqlCommand getClassDescCommand = new SqlCommand("DECLARE @getCD TABLE(classDesc nvarchar(50)); " +
                                                                "INSERT INTO @getCD SELECT cl.ClassDescription " +
                                                                "FROM Crew AS c " +
                                                                "LEFT JOIN Class AS cl ON cl.ClassID = c.ClassID" +
                                                                "SELECT * " +
                                                                "FROM @getCD ");

                SqlDataReader getClassDescReader = getClassDescCommand.ExecuteReader();

                List<string> classDescList = new List<string>();

                while (getClassDescReader.Read())
                {
                    classDescList.Add(getClassDescReader["ClassDescription"].ToString());
                }
                for (int i = 0; i < classDescList.Count; i++)
                {
                    if (classDescList[i].ToString() == "NULL")
                    {
                        classDescList[i] = "Ship Maintanence";
                    }
                }
                conn.Close();
                //------------here is a try to change the null value found in the classDescription
                //----column. As well as the classDescList.ToString() found down below. If there is
                //---a need to change back, remove the above code and remove the first two lines
                //--in the string below

                comm.CommandText = "UPDATE Class " +
                                   "SET ClassDescription = " + classDescList.ToString() + " " +
                                   "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, " +
                                   "cl.ClassDescription AS WorkedFor, c.Job AS WorkedAs " +
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
