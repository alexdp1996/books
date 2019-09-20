﻿CREATE PROCEDURE [dbo].[USP_Author_Get_By_Name_DESC]
	@Start INT,
	@Lenght INT
AS
	DECLARE @Skip INT;

	SET @Skip = @Start*@Lenght;

	DROP TABLE IF EXISTS #Authors;
	
	CREATE TABLE #Authors
	(
		[Id] BIGINT,
		[Name] NVARCHAR(30),
		[Surname] NVARCHAR(30)
	);

	INSERT INTO #Authors
	SELECT * FROM Author
	ORDER BY [Name] DESC
	OFFSET @Skip ROWS
	FETCH NEXT @Lenght ROWS ONLY;

	EXEC [dbo].[USP_Author_Get_DataTable] @Skip = @Skip;

RETURN 0