CREATE PROCEDURE [dbo].[USP_Author_Delete]
	@Id int
AS
	DELETE FROM AuthorBook WHERE BookId = @Id;
	DELETE FROM Author WHERE Id = @Id;
RETURN 0
