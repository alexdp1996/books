CREATE PROCEDURE [dbo].[USPBookGetList]
	@Start INT,
	@Lenght INT,
	@OrderColumnName VARCHAR(50),
	@IsAsc BIT
AS
	DROP TABLE IF EXISTS #Books;
	
	CREATE TABLE #Books
	(
		[Id]            BIGINT, 
		[Name]          NVARCHAR (50),
		[Rate]          INT,
		[Pages]         INT,
		[CreatedDate]          DATETIME
	);

	INSERT INTO #Books
	SELECT B.Id, B.[Name], B.Rate, B.Pages, B.[CreatedDate] FROM Book B
	ORDER BY 
		CASE WHEN @OrderColumnName = 'Name' AND @IsAsc = 1 THEN B.[Name] END ASC,
		CASE WHEN @OrderColumnName = 'Rate' AND @IsAsc = 1  THEN B.[Rate]  END ASC,
		CASE WHEN @OrderColumnName = 'Pages' AND @IsAsc = 1  THEN B.[Pages] END ASC,
		CASE WHEN @OrderColumnName = 'CreatedDate' AND @IsAsc = 1  THEN B.[CreatedDate] END ASC,
		CASE WHEN @OrderColumnName = 'Name' AND @IsAsc = 0 THEN B.[Name] END DESC,
		CASE WHEN @OrderColumnName = 'Rate' AND @IsAsc = 0 THEN B.[Rate] END DESC,
		CASE WHEN @OrderColumnName = 'Pages' AND @IsAsc = 0 THEN B.[Pages] END DESC,
		CASE WHEN @OrderColumnName = 'CreatedDate' AND @IsAsc = 0 THEN B.[CreatedDate] END DESC
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
	FROM #Books B
	JOIN AuthorBook AB ON AB.BookId = B.Id

	SELECT B.Id, B.[Name], B.[CreatedDate], B.Pages, B.Rate FROM #Books B

	SELECT BookId, AuthorId FROM #AuthorBook

	SELECT DISTINCT A.Id, A.[Name], A.Surname
	FROM #AuthorBook AB
	JOIN Author A ON AB.AuthorId = A.Id

	DROP TABLE #Books;
	DROP TABLE #AuthorBook;

RETURN 0