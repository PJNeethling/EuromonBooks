CREATE OR ALTER PROC DeleteUserBook
	@Uuid UNIQUEIDENTIFIER,
	@BookId INT
AS
BEGIN
	BEGIN TRY
		DECLARE @UserId INT

		SELECT @UserId = u.Id 
		FROM [User] u
		WHERE u.Uuid = @Uuid
		
		IF @UserId IS NULL
			THROW 50000, N'User not found', 1;

		IF NOT EXISTS (SELECT * 
					FROM UserBook ub
					WHERE ub.BookId = @BookId
						AND ub.UserId = @UserId)
			THROW 50000, N'Book not found', 1;

		BEGIN TRAN

			DELETE FROM [UserBook]
			WHERE UserId = @UserId
				AND BookId = @BookId

		COMMIT

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK;
		THROW;
	END CATCH
END