CREATE PROCEDURE [dbo].[USP_Book_Get_DataTable_By_Rate_ASC]
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
		[Date]          DATETIME,
		[Discriminator] NVARCHAR (128)
	);

	INSERT INTO #Books
	SELECT * FROM Book
	ORDER BY [Rate] ASC
	OFFSET @Skip ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	EXEC [dbo].[USP_Book_Get_DataTable] @Skip = @Skip;

RETURN 0