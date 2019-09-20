CREATE PROCEDURE [dbo].[USP_Book_Delete]
	@id int
AS
	DELETE FROM AuthorBook WHERE BookId = @id;
	DELETE FROM Book WHERE Id = @id;
RETURN 0
