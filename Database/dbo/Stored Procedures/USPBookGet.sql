﻿CREATE PROCEDURE [dbo].[USPBookGet]
	@Id int
AS
	SELECT B.Id, B.[Name], B.[CreatedDate], B.Pages, B.Rate FROM Book B
	WHERE B.Id = @Id

	SELECT A.Id, A.[Name], A.Surname FROM AuthorBook AB
	JOIN Author A ON A.Id = AB.AuthorId
	WHERE AB.BookId = @Id
RETURN 0
