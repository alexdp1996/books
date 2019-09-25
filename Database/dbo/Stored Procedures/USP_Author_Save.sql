CREATE PROCEDURE [dbo].[USP_Author_Save]
	@Id            BIGINT,
    @Name          NVARCHAR (50),
    @Surname       NVARCHAR (50)
AS
	
	IF @Id = 0
	BEGIN
		INSERT INTO Author ([Name],[Surname])
		VALUES (@Name, @Surname);

		SET @Id = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Author SET 
						[Name] = @Name,
						[Surname] = @Surname
		WHERE Id = @Id
	END

	SELECT @Id

RETURN 0
