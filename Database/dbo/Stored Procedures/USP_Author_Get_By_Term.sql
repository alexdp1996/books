CREATE PROCEDURE [dbo].[USP_Author_Get_By_Term]
	@Term VARCHAR(MAX)
AS
	SELECT * FROM Author
	WHERE CONCAT([Name],' ',[Surname]) LIKE CONCAT('%',@Term,'%')
RETURN 0
