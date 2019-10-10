CREATE PROCEDURE [dbo].[USPAuthorGetDataTableBySurnameDESC]
	@Start INT,
	@Lenght INT
AS
	DECLARE @Skip INT;

	SET @Skip = @Start*@Lenght;

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
	OFFSET @Skip ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	EXEC [dbo].[USPAuthorGetDataTable] @Skip = @Skip;

RETURN 0