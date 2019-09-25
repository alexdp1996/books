CREATE PROCEDURE [dbo].[USP_Book_Save]
	@Id            BIGINT,
    @Name          NVARCHAR (50),
    @Rate          INT,
    @Pages         INT,
    @Date          DATETIME,
	@AuthorIds	   BigIntList READONLY
AS
	BEGIN TRY
		BEGIN TRANSACTION

			IF @Id = 0
			BEGIN
				INSERT INTO Book ([Name],[Rate],[Pages],[Date])
				VALUES (@Name, @Rate, @Pages, @Date);

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

				DELETE FROM AuthorBook WHERE BookId = @Id;
			END

			INSERT INTO AuthorBook (BookId, AuthorId)
			SELECT @Id, Element
			FROM @AuthorIds;

			SELECT @Id

		COMMIT TRANSACTION
	
	END TRY
	BEGIN CATCH

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION

		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()

		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);

	END CATCH

RETURN 0
