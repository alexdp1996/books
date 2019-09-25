CREATE PROCEDURE [dbo].[USP_Author_Get]
	@Id int
AS
	SELECT A.* FROM Author A
	WHERE A.Id = @Id

	SELECT B.* FROM AuthorBook AB
	JOIN Book B ON B.Id = AB.BookId
	WHERE AB.AuthorId = @Id
RETURN 0
