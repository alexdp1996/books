CREATE PROCEDURE [dbo].[USPAuthorGet]
	@Id int
AS
	SELECT A.Id, A.[Name], A.Surname FROM Author A
	WHERE A.Id = @Id

	SELECT B.Id, B.[Name], B.[CreatedDate], B.Pages, B.Rate FROM AuthorBook AB
	JOIN Book B ON B.Id = AB.BookId
	WHERE AB.AuthorId = @Id
RETURN 0
