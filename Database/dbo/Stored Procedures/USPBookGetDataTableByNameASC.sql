CREATE PROCEDURE [dbo].[USPBookGetDataTableByNameASC]
	@Start INT,
	@Lenght INT
AS
	DECLARE @Skip INT;

	SET @Skip = @Start*@Lenght;

	DROP TABLE IF EXISTS #Books;
	
	CREATE TABLE #Books
	(
		[Id]            BIGINT, 
		[Name]          NVARCHAR (50),
		[Rate]          INT,
		[Pages]         INT,
		[Date]          DATETIME
	);

	INSERT INTO #Books
	SELECT B.Id, B.[Name], B.Rate, B.Pages, B.[Date] FROM Book B
	ORDER BY [Name] ASC
	OFFSET @Skip ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	EXEC [dbo].[USPBookGetDataTable] @Skip = @Skip;

RETURN 0