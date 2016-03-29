﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

public partial class TitanicEntities : DbContext
{
    public TitanicEntities()
        : base("name=TitanicEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public DbSet<Cabin> Cabin { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<Crew> Crew { get; set; }
    public DbSet<DepartCity> DepartCity { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Passenger> Passenger { get; set; }

    public virtual int CabinPassenger(string name)
    {
        var nameParameter = name != null ?
            new ObjectParameter("name", name) :
            new ObjectParameter("name", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CabinPassenger", nameParameter);
    }

    public virtual int CrewClass(string name)
    {
        var nameParameter = name != null ?
            new ObjectParameter("name", name) :
            new ObjectParameter("name", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CrewClass", nameParameter);
    }

    public virtual int CrewDepartment(string name)
    {
        var nameParameter = name != null ?
            new ObjectParameter("name", name) :
            new ObjectParameter("name", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CrewDepartment", nameParameter);
    }

    public virtual int GetAllInCrew()
    {
        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllInCrew");
    }

    public virtual int GetClassOfPassenger(string firstname, string lastname)
    {
        var firstnameParameter = firstname != null ?
            new ObjectParameter("Firstname", firstname) :
            new ObjectParameter("Firstname", typeof(string));

        var lastnameParameter = lastname != null ?
            new ObjectParameter("Lastname", lastname) :
            new ObjectParameter("Lastname", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetClassOfPassenger", firstnameParameter, lastnameParameter);
    }

    public virtual int GetClassOfPassengers(string firstname, string lastname)
    {
        var firstnameParameter = firstname != null ?
            new ObjectParameter("Firstname", firstname) :
            new ObjectParameter("Firstname", typeof(string));

        var lastnameParameter = lastname != null ?
            new ObjectParameter("Lastname", lastname) :
            new ObjectParameter("Lastname", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetClassOfPassengers", firstnameParameter, lastnameParameter);
    }

    public virtual int GetCrewFromCity(Nullable<int> cityID)
    {
        var cityIDParameter = cityID.HasValue ?
            new ObjectParameter("CityID", cityID) :
            new ObjectParameter("CityID", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetCrewFromCity", cityIDParameter);
    }

    public virtual int GetPassengerFromCity(Nullable<int> cityID)
    {
        var cityIDParameter = cityID.HasValue ?
            new ObjectParameter("CityID", cityID) :
            new ObjectParameter("CityID", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetPassengerFromCity", cityIDParameter);
    }

    public virtual int InsertCrew(string lastname, string firstname, Nullable<int> age, Nullable<int> departmentID, Nullable<int> cityID, string job, Nullable<int> classID)
    {
        var lastnameParameter = lastname != null ?
            new ObjectParameter("Lastname", lastname) :
            new ObjectParameter("Lastname", typeof(string));

        var firstnameParameter = firstname != null ?
            new ObjectParameter("Firstname", firstname) :
            new ObjectParameter("Firstname", typeof(string));

        var ageParameter = age.HasValue ?
            new ObjectParameter("Age", age) :
            new ObjectParameter("Age", typeof(int));

        var departmentIDParameter = departmentID.HasValue ?
            new ObjectParameter("DepartmentID", departmentID) :
            new ObjectParameter("DepartmentID", typeof(int));

        var cityIDParameter = cityID.HasValue ?
            new ObjectParameter("CityID", cityID) :
            new ObjectParameter("CityID", typeof(int));

        var jobParameter = job != null ?
            new ObjectParameter("Job", job) :
            new ObjectParameter("Job", typeof(string));

        var classIDParameter = classID.HasValue ?
            new ObjectParameter("ClassID", classID) :
            new ObjectParameter("ClassID", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertCrew", lastnameParameter, firstnameParameter, ageParameter, departmentIDParameter, cityIDParameter, jobParameter, classIDParameter);
    }

    public virtual int InsertPassenger(string lastname, string firstname, string age, Nullable<int> cabinID, string ticket, string ticketPrice, Nullable<int> cityID, string job)
    {
        var lastnameParameter = lastname != null ?
            new ObjectParameter("Lastname", lastname) :
            new ObjectParameter("Lastname", typeof(string));

        var firstnameParameter = firstname != null ?
            new ObjectParameter("Firstname", firstname) :
            new ObjectParameter("Firstname", typeof(string));

        var ageParameter = age != null ?
            new ObjectParameter("Age", age) :
            new ObjectParameter("Age", typeof(string));

        var cabinIDParameter = cabinID.HasValue ?
            new ObjectParameter("CabinID", cabinID) :
            new ObjectParameter("CabinID", typeof(int));

        var ticketParameter = ticket != null ?
            new ObjectParameter("Ticket", ticket) :
            new ObjectParameter("Ticket", typeof(string));

        var ticketPriceParameter = ticketPrice != null ?
            new ObjectParameter("TicketPrice", ticketPrice) :
            new ObjectParameter("TicketPrice", typeof(string));

        var cityIDParameter = cityID.HasValue ?
            new ObjectParameter("CityID", cityID) :
            new ObjectParameter("CityID", typeof(int));

        var jobParameter = job != null ?
            new ObjectParameter("Job", job) :
            new ObjectParameter("Job", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertPassenger", lastnameParameter, firstnameParameter, ageParameter, cabinIDParameter, ticketParameter, ticketPriceParameter, cityIDParameter, jobParameter);
    }

    public virtual int SearchByAge(string age)
    {
        var ageParameter = age != null ?
            new ObjectParameter("age", age) :
            new ObjectParameter("age", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SearchByAge", ageParameter);
    }
}
