CREATE PROCEDURE [dbo].[USP_Book_Get]
	@Id int
AS
	SELECT B.* FROM Book B
	WHERE B.Id = @Id

	SELECT A.* FROM AuthorBook AB
	JOIN Author A ON A.Id = AB.AuthorId
	WHERE AB.BookId = @Id
RETURN 0
