using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public class LambrantSprocs
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static SqlInt32 SearchByAge(SqlInt32 age)
    {
        using (SqlConnection conn = new SqlConnection("context connection=true"))
        {
            SqlCommand comm = new SqlCommand();

            if (age < 0 || age > 120)
                return 0;

            comm.CommandText = "SELECT p.Lastname, p.Firstname, p.Age" + 
                                "FROM Passenger AS p" +
                                "WHERE p.Age < " + age + ";" +
                                "SELECT cr.Lastname, cr.Firstname, cr.Age" +
                                "FROM Crew AS cr" + 
                                "WHERE cr.Age < " + age + ";";

            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
    }

}
