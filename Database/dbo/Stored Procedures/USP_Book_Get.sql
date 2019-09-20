CREATE PROCEDURE [dbo].[USP_Book_Get]
	@id int
AS
	SELECT B.*, A.* FROM Book B
	JOIN AuthorBook AB ON AB.BookId = B.Id
	JOIN Author A ON A.Id = AB.AuthorId
	WHERE B.Id = @id
RETURN 0
