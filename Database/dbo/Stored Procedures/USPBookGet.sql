CREATE PROCEDURE [dbo].[USP_Book_Get]
	@Id int
AS
	SELECT B.Id, B.[Name], B.[Date], B.Pages, B.Rate FROM Book B
	WHERE B.Id = @Id

	SELECT A.Id, A.[Name], A.Surname FROM AuthorBook AB
	JOIN Author A ON A.Id = AB.AuthorId
	WHERE AB.BookId = @Id
RETURN 0
