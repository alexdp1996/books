CREATE PROCEDURE [dbo].[USP_Book_Save]
	@Id            BIGINT,
    @Name          NVARCHAR (50),
    @Rate          INT,
    @Pages         INT,
    @Date          DATETIME,
	@AuthorIds	   BigIntList READONLY
AS
	
	IF @Id = 0
	BEGIN
		INSERT INTO Book ([Name],[Rate],[Pages],[Date],[Discriminator])
		VALUES (@Name, @Rate, @Pages, @Date, 'UpdatableBookEM');

		SET @Id = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Book SET 
					[Name] = @Name,
					[Rate] = @Rate,
					[Pages] = @Pages,
					[Date] = @Date
		WHERE Id = @Id
	END

	DELETE FROM AuthorBook WHERE BookId = @Id;

	INSERT INTO AuthorBook (BookId, AuthorId)
	SELECT @Id, Element
	FROM @AuthorIds;

	SELECT @Id

RETURN 0
