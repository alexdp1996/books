CREATE PROCEDURE [dbo].[USPAuthorGetDataTableBySurnameDESC]
	@Start INT,
	@Lenght INT
AS
	DROP TABLE IF EXISTS #Authors;
	
	CREATE TABLE #Authors
	(
		[Id] BIGINT,
		[Name] NVARCHAR(30),
		[Surname] NVARCHAR(30)
	);

	INSERT INTO #Authors
	SELECT A.[Id], A.[Name], A.[Surname] FROM Author A
	ORDER BY [Name] DESC
	OFFSET @Start ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	DECLARE @RecordsFiltered INT;

	SELECT @RecordsFiltered = COUNT(*) FROM Author;

	EXEC [dbo].[USPAuthorGetDataTable] @RecordsFiltered = @RecordsFiltered;

RETURN 0