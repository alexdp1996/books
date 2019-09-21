CREATE PROCEDURE [dbo].[USP_Book_DataTable_Get_DataTable]
	@Skip INT
AS
	DECLARE @RecordsFiltered INT,
			@RecordsTotal INT;

	SELECT @RecordsTotal = COUNT(*) FROM Book;
	SELECT @RecordsFiltered = COUNT(*) FROM #Books;
	SET @RecordsFiltered = @RecordsFiltered + @Skip;

	SELECT @RecordsFiltered AS RecordsFiltered, @RecordsTotal AS RecordsTotal;

	CREATE TABLE #AuthorBook
	(
		[BookId] BIGINT,
		[AuthorId] BIGINT,
	); 

	INSERT INTO #AuthorBook ([BookId], [AuthorId])
	SELECT AB.BookId, AB.AuthorId
	FROM #Books B
	JOIN AuthorBook AB ON AB.BookId = B.Id

	SELECT * FROM #Books

	SELECT * FROM #AuthorBook

	SELECT DISTINCT A.*
	FROM #AuthorBook AB
	JOIN Author A ON AB.AuthorId = A.Id

	DROP TABLE #Books;
	DROP TABLE #AuthorBook;

RETURN 0