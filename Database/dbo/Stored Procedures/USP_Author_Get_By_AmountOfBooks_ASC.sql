CREATE PROCEDURE [dbo].[USP_Author_Get_By_AmountOfBooks_ASC]
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
	SELECT [Id], [Name], [Surname] FROM Author A
	LEFT JOIN AuthorBook AB on A.Id = AB.AuthorId
	GROUP BY A.Id, A.Name, A.Surname
	ORDER BY COUNT(AB.BookId) ASC
	OFFSET @Skip ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	EXEC [dbo].[USP_Author_Get_DataTable] @Skip = @Skip;

RETURN 0