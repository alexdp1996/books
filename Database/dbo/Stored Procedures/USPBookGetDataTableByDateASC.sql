CREATE PROCEDURE [dbo].[USPBookGetDataTableByDateASC]
	@Start INT,
	@Lenght INT
AS
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
	ORDER BY [Date] ASC
	OFFSET @Start ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	DECLARE @RecordsFiltered INT;

	SELECT @RecordsFiltered = COUNT(*) FROM Book;

	EXEC [dbo].[USPBookGetDataTable] @RecordsFiltered = @RecordsFiltered;

RETURN 0