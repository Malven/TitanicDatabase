using System;
using System.Collections.Generic;
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

