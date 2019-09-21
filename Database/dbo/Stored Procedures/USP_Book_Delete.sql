CREATE PROCEDURE [dbo].[USP_Book_Delete]
	@Id int
AS
	DELETE FROM AuthorBook WHERE BookId = @Id;
	DELETE FROM Book WHERE Id = @Id;
RETURN 0
