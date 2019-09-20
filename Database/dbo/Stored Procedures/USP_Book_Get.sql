CREATE PROCEDURE [dbo].[USP_Book_Get]
	@id int
AS
	SELECT B.* FROM Book B
	WHERE B.Id = @id

	SELECT A.* FROM AuthorBook AB
	JOIN Author A ON A.Id = AB.AuthorId
	WHERE AB.BookId = @id
RETURN 0
