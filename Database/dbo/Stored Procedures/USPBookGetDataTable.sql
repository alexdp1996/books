CREATE PROCEDURE [dbo].[USPBookGetDataTable]
	@RecordsFiltered INT
AS
	DECLARE @RecordsTotal INT;

	SELECT @RecordsTotal = COUNT(*) FROM Book;

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

	SELECT B.Id, B.[Name], B.[Date], B.Pages, B.Rate FROM #Books B

	SELECT BookId, AuthorId FROM #AuthorBook

	SELECT DISTINCT A.Id, A.[Name], A.Surname
	FROM #AuthorBook AB
	JOIN Author A ON AB.AuthorId = A.Id

	DROP TABLE #Books;
	DROP TABLE #AuthorBook;

RETURN 0