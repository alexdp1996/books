CREATE PROCEDURE [dbo].[USPBookUpdateAuthors]
	@BookId            BIGINT,
	@AuthorIds	   BigIntList READONLY
AS
	BEGIN TRY
		BEGIN TRANSACTION

			DELETE FROM AuthorBook WHERE BookId = @BookId;

			INSERT INTO AuthorBook (BookId, AuthorId)
			SELECT @BookId, Element
			FROM @AuthorIds;

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
