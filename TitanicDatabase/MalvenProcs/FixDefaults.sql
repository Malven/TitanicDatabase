/*
Fixar default värden på vissa parametrar, SSDT stödjer tydligen inte defaulta värden när den automatiserar publish
*/
GO
ALTER PROCEDURE [dbo].[InsertPassenger]
	@Lastname [nvarchar](50),
	@Firstname [nvarchar](50),
	@Age [nvarchar](4),
	@CabinID [int] = 1,
	@Ticket [nvarchar](6),
	@TicketPrice [nvarchar](10),
	@CityID [int] = 1,
	@Job [nvarchar](50)
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [TitanicDatabase].[StoredProcedures].[InsertPassenger]

GO
ALTER PROCEDURE [dbo].[InsertCrew]
	@Lastname [nvarchar](50),
	@Firstname [nvarchar](50),
	@Age [int] = null,
	@DepartmentID [int] = 1,
	@CityID [int] = 1,
	@Job [nvarchar](50),
	@ClassID [int] = 1
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [TitanicDatabase].[StoredProcedures].[InsertCrew]
GO

ALTER PROCEDURE [dbo].[GetPassengerFromCity]
@CityID INT = null
WITH EXECUTE AS CALLER
AS EXTERNAL NAME [TitanicDatabase].[StoredProcedures].[GetPassengerFromCity]
GO

ALTER PROCEDURE [dbo].[GetCrewFromCity]
@CityID INT = null
WITH EXECUTE AS CALLER
AS EXTERNAL NAME [TitanicDatabase].[StoredProcedures].[GetCrewFromCity]
GO