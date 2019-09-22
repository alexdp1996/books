CREATE PROCEDURE [dbo].[USP_Author_Get_DataTable]
	@Skip INT
AS
	DECLARE @RecordsFiltered INT,
			@RecordsTotal INT;

	SELECT @RecordsTotal = COUNT(*) FROM Author;
	SELECT @RecordsFiltered = COUNT(*) FROM #Authors;
	SET @RecordsFiltered = @RecordsFiltered + @Skip;

	SELECT @RecordsFiltered AS RecordsFiltered, @RecordsTotal AS RecordsTotal;

	CREATE TABLE #AuthorBook
	(
		[BookId] BIGINT,
		[AuthorId] BIGINT,
	); 

	INSERT INTO #AuthorBook ([BookId], [AuthorId])
	SELECT AB.BookId, AB.AuthorId
	FROM #Authors A
	JOIN AuthorBook AB ON AB.AuthorId = A.Id

	SELECT * FROM #Authors

	SELECT * FROM #AuthorBook

	SELECT DISTINCT B.*
	FROM #AuthorBook AB
	JOIN Book B ON AB.BookId = B.Id

	DROP TABLE #Authors;
	DROP TABLE #AuthorBook;

RETURN 0