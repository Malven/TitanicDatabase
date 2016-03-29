using System;
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

            SqlParameter ageParam = new SqlParameter();
            ageParam.Direction = ParameterDirection.Input;
            ageParam.ParameterName = "@Age";
            ageParam.SqlDbType = SqlDbType.Int;
            ageParam.SqlValue = age;
            comm.Parameters.Add(age);

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
                                    "WHERE Age = @Age UNION ALL " +
                                    "SELECT COALESCE(COALESCE(Lastname + ', ', '') + Firstname, Lastname) AS FullName, Age " +
                                    "FROM Crew " +
                                    "WHERE Age = @Age;";

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

            SqlParameter nameParam = new SqlParameter();
            nameParam.Direction = ParameterDirection.Input;
            nameParam.ParameterName = "@Name";
            nameParam.SqlDbType = SqlDbType.NVarChar;
            nameParam.SqlValue = name;
            comm.Parameters.Add(name);

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
                                   "WHERE p.Lastname = @Name;";

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

            SqlParameter nameParam = new SqlParameter();
            nameParam.Direction = ParameterDirection.Input;
            nameParam.ParameterName = "@Name";
            nameParam.SqlDbType = SqlDbType.NVarChar;
            nameParam.SqlValue = name;
            comm.Parameters.Add(name);

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
                                "WHERE c.Lastname = @Name;";

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
            //SqlString[] classNull = new SqlString[1104];

            SqlParameter nameParam = new SqlParameter();
            nameParam.Direction = ParameterDirection.Input;
            nameParam.ParameterName = "@Name";
            nameParam.SqlDbType = SqlDbType.NVarChar;
            nameParam.SqlValue = name;
            comm.Parameters.Add(name);

            if (name.ToString() == "")
            {
                //loop section to use in web, will take out a part of the database.
                /*conn.Open();
                SqlCommand getClassDescCommand = new SqlCommand("DECLARE @getCD TABLE(classDesc nvarchar(50)) " +
                                                                "INSERT INTO @getCD SELECT cl.ClassDescription " +
                                                                "FROM Crew AS c " +
                                                                "LEFT JOIN Class AS cl ON cl.ClassID = c.ClassID " +
                                                                "SELECT * " + 
                                                                "FROM @getCD ", conn);

                SqlDataReader getClassDescReader = getClassDescCommand.ExecuteReader();

                List<string> classDescList = new List<string>();

                while (getClassDescReader.Read())
                {
                    classDescList.Add(getClassDescReader["classDesc"].ToString());
                }
                for (int i = 0; i < classDescList.Count; i++)
                {
                    if (classDescList[i].ToString() == "NULL")
                    {
                        classDescList[i] = "Ship Maintanence";
                    }
                }
                conn.Close();*/

                comm.CommandText = "SELECT COALESCE(COALESCE(c.Lastname + ', ', '') + c.Firstname, c.Lastname) AS FullName, " +
                                   "CASE WHEN c.ClassID IS NULL THEN 'Ship Maintanance' ELSE cl.ClassDescription END AS 'WorkedFor', " +
                                   "c.Job " +
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
                                   "WHERE c.Lastname = @Name " +
                                   "AND c.ClassID IS NULL OR " +
                                   "c.Lastname = @Name " +
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
