CREATE PROCEDURE [dbo].[USPAuthorGetList]
	@Start INT,
	@Lenght INT,
	@OrderColumnName VARCHAR(50),
	@IsAsc BIT
AS
	DROP TABLE IF EXISTS #Authors;
	
	CREATE TABLE #Authors
	(
		[Id] BIGINT,
		[Name] NVARCHAR(30),
		[Surname] NVARCHAR(30)
	);

	INSERT INTO #Authors
	SELECT R.[Id], R.[Name], R.[Surname]
	FROM (
		SELECT A.[Id], A.[Name], A.[Surname], COUNT(AB.BookId) AS CountOfBooks FROM Author A
		LEFT JOIN AuthorBook AB on A.Id = AB.AuthorId
		GROUP BY A.Id, A.Name, A.Surname
	) R
	ORDER BY 
		CASE WHEN @OrderColumnName = 'Name' AND @IsAsc = 1 THEN R.[Name] END ASC,
		CASE WHEN @OrderColumnName = 'Surname' AND @IsAsc = 1  THEN R.[Surname]  END ASC,
		CASE WHEN @OrderColumnName = 'CountOfBooks' AND @IsAsc = 1  THEN R.[CountOfBooks] END ASC,
		CASE WHEN @OrderColumnName = 'Name' AND @IsAsc = 0 THEN R.[Name] END DESC,
		CASE WHEN @OrderColumnName = 'Surname' AND @IsAsc = 0  THEN R.[Surname]  END DESC,
		CASE WHEN @OrderColumnName = 'CountOfBooks' AND @IsAsc = 0  THEN R.[CountOfBooks] END DESC
	OFFSET @Start ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	DECLARE @RecordsFiltered INT,
			@RecordsTotal INT;

	SELECT @RecordsFiltered = COUNT(*), @RecordsTotal = COUNT(*)
	FROM Author;

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

	SELECT DISTINCT B.Id, B.[Name], B.[CreatedDate], B.Pages, B.Rate
	FROM #AuthorBook AB
	JOIN Book B ON AB.BookId = B.Id

	DROP TABLE #Authors;
	DROP TABLE #AuthorBook;

RETURN 0