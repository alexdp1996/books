CREATE PROCEDURE [dbo].[USPAuthorGetByTerm]
	@Term VARCHAR(MAX)
AS
	SELECT Id, [Name], Surname FROM Author
	WHERE CONCAT([Name],' ',[Surname]) LIKE CONCAT('%',@Term,'%')
RETURN 0
