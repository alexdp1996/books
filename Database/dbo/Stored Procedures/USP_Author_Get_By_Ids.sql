CREATE PROCEDURE [dbo].[USP_Author_Get_By_Ids]
	@Ids BigIntList READONLY
AS
	SELECT A.* FROM Author A
	JOIN @Ids I ON A.Id = I.Element
RETURN 0
