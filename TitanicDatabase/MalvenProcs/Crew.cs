using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;


public class Crew
{
    public SqlInt32 CrewID { get; set; }
    public SqlString Firstname { get; set; }
    public SqlString Lastname { get; set; }
    public SqlInt32 Age { get; set; }
    public SqlInt32? DepartmentID { get; set; }
    public SqlInt32? CityID { get; set; }
    public SqlString? Job { get; set; }
    public SqlInt32? ClassID { get; set; }

    public Crew(SqlString _firstname, SqlString _lastname, SqlInt32 _age, SqlInt32 _departmentID, SqlInt32 _cityID, SqlString _job, SqlInt32 _classID )
    {
        Random rnd = new Random(1234);
        CrewID = DateTime.Now.Year + rnd.Next(1000, 10000);
        Firstname = _firstname;
        Lastname = _lastname;
        Age = _age;
        DepartmentID = _departmentID;
        CityID = _cityID;
        Job = _job;
        ClassID = _classID;
    }
    public bool CheckInputs()
    {
        int tempAge;
        bool isNum = int.TryParse(Age.ToString(), out tempAge);

        if (tempAge < 0 || tempAge > 120 || !isNum)
            return false;

        if (Firstname.ToString() == null || Lastname.ToString() == null)
            return false;

        return true;
    }
}

