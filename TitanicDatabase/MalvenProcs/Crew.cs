using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;


public class Crew
{
    public SqlString Firstname { get; set; }
    public SqlString Lastname { get; set; }
    public SqlInt32 Age { get; set; }
    public SqlInt32? DepartmentID { get; set; }
    public SqlInt32? CityID { get; set; }
    public SqlString? Job { get; set; }
    public SqlInt32? ClassID { get; set; }
    public List<SqlParameter> paramList = new List<SqlParameter>();

    public Crew(SqlString _firstname, SqlString _lastname, SqlInt32 _age, SqlInt32 _departmentID, SqlInt32 _cityID, SqlString _job, SqlInt32 _classID )
    {
        Firstname = _firstname;
        Lastname = _lastname;
        Age = _age;
        DepartmentID = _departmentID;
        CityID = _cityID;
        Job = _job;
        ClassID = _classID;

        SqlParameter paramFirstname = new SqlParameter( "@Firstname", System.Data.SqlDbType.NVarChar );
        paramFirstname.Value = Firstname;
        paramFirstname.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramFirstname );

        SqlParameter paramLastname = new SqlParameter( "@Lastname", System.Data.SqlDbType.NVarChar );
        paramLastname.Value = Lastname;
        paramLastname.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramLastname );

        SqlParameter paramAge = new SqlParameter( "@Age", System.Data.SqlDbType.Int );
        paramAge.Value = Age;
        paramAge.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramAge );

        SqlParameter paramDepartmentID = new SqlParameter( "@DepartmentID", System.Data.SqlDbType.Int );
        paramDepartmentID.Value = DepartmentID;
        paramDepartmentID.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramDepartmentID );

        SqlParameter paramCityID = new SqlParameter( "@CityID", System.Data.SqlDbType.Int );
        paramCityID.Value = CityID;
        paramCityID.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramCityID );

        SqlParameter paramJob = new SqlParameter( "@Job", System.Data.SqlDbType.NVarChar );
        paramJob.Value = Job;
        paramJob.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramJob );

        SqlParameter paramClassID = new SqlParameter( "@ClassID", System.Data.SqlDbType.Int );
        paramClassID.Value = ClassID;
        paramClassID.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramClassID );

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

