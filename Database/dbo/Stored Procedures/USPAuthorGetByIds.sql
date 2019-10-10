CREATE PROCEDURE [dbo].[USPAuthorGetByIds]
	@Ids BigIntList READONLY
AS
	SELECT A.Id, A.[Name], A.Surname FROM Author A
	JOIN @Ids I ON A.Id = I.Element
RETURN 0
