CREATE PROCEDURE [dbo].[USPAuthorGetDataTable]
	@RecordsFiltered INT
AS
	DECLARE @RecordsTotal INT;

	SELECT @RecordsTotal = COUNT(*) FROM Author;

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

	SELECT A.Id, A.[Name], A.Surname FROM #Authors A

	SELECT BookId, AuthorId FROM #AuthorBook

	SELECT DISTINCT B.Id, B.[Name], B.[Date], B.Pages, B.Rate
	FROM #AuthorBook AB
	JOIN Book B ON AB.BookId = B.Id

	DROP TABLE #Authors;
	DROP TABLE #AuthorBook;

RETURN 0