using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;


public class Passenger
{
    public SqlString Firstname { get; set; }
    public SqlString Lastname { get; set; }
    public SqlString Age { get; set; }
    public SqlInt32? CabinID { get; set; }
    public SqlString? Ticket { get; set; }
    public SqlString? TicketPrice { get; set; }
    public SqlInt32? CityID { get; set; }
    public SqlString? Job { get; set; }
    public List<SqlParameter> paramList = new List<SqlParameter>();

    public Passenger(SqlString _firstname, SqlString _lastname, SqlString _age, SqlInt32 _cabinID, SqlString _ticket, SqlString _ticketPrice, SqlInt32 _cityID, SqlString _job)
    {
        Firstname = _firstname;
        Lastname = _lastname;
        Age = _age;
        CabinID = _cabinID;
        Ticket = _ticket;
        TicketPrice = _ticketPrice;
        CityID = _cityID;
        Job = _job;

        SqlParameter paramFirstname = new SqlParameter( "@Firstname", System.Data.SqlDbType.NVarChar );
        paramFirstname.Value = Firstname;
        paramFirstname.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramFirstname );

        SqlParameter paramLastname = new SqlParameter( "@Lastname", System.Data.SqlDbType.NVarChar );
        paramLastname.Value = Lastname;
        paramLastname.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramLastname );

        SqlParameter paramAge = new SqlParameter( "@Age", System.Data.SqlDbType.NVarChar );
        paramAge.Value = Age;
        paramAge.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramAge );

        SqlParameter paramCabinID = new SqlParameter( "@CabinID", System.Data.SqlDbType.Int );
        paramCabinID.Value = CabinID;
        paramCabinID.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramCabinID );

        SqlParameter paramTicket = new SqlParameter( "@Ticket", System.Data.SqlDbType.NVarChar );
        paramTicket.Value = Ticket;
        paramTicket.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramTicket );

        SqlParameter paramTicketPrice = new SqlParameter( "@TicketPrice", System.Data.SqlDbType.NVarChar );
        paramTicketPrice.Value = TicketPrice;
        paramTicketPrice.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramTicketPrice );

        SqlParameter paramCityID = new SqlParameter( "@CityID", System.Data.SqlDbType.Int );
        paramCityID.Value = CityID;
        paramCityID.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramCityID );

        SqlParameter paramJob = new SqlParameter( "@Job", System.Data.SqlDbType.NVarChar );
        paramJob.Value = Job;
        paramJob.Direction = System.Data.ParameterDirection.Input;
        paramList.Add( paramJob );

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

