CREATE PROCEDURE [dbo].[USP_Author_Get_List]
	@Skip INT
AS
	DECLARE @RecordsFiltered INT,
			@RecordsTotal INT;

	SELECT @RecordsTotal = COUNT(*) FROM Author;
	SELECT @RecordsFiltered = COUNT(*) FROM #Authors;
	SET @RecordsFiltered = @RecordsFiltered + @Skip;

	SELECT A.*, B.*
	FROM #Authors A
	LEFT JOIN AuthorBook AB ON AB.AuthorId = A.Id
	LEFT JOIN Book B ON AB.BookId = B.Id

	SELECT @RecordsFiltered AS RecordsFiltered, @RecordsTotal AS RecordsTotal;

	DROP TABLE #Authors;

RETURN 0