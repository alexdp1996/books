CREATE PROCEDURE [dbo].[USPBookGetDataTableByPagesDESC]
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
	ORDER BY [Pages] DESC
	OFFSET @Start ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	DECLARE @RecordsFiltered INT;

	SELECT @RecordsFiltered = COUNT(*) FROM Book;

	EXEC [dbo].[USPBookGetDataTable] @RecordsFiltered = @RecordsFiltered;

RETURN 0