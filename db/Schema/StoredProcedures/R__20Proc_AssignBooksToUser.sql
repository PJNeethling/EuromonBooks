CREATE OR ALTER PROC AssignBooksToUser
	@Uuid UNIQUEIDENTIFIER,
	@BookIds IdListType READONLY
AS
BEGIN
	BEGIN TRY
		DECLARE @UserId INT

		SELECT @UserId = u.Id 
		FROM [User] u
		WHERE u.Uuid = @Uuid


		IF @UserId IS NULL
			THROW 50000, N'User not found', 1;

		IF EXISTS (SELECT * FROM @BookIds ri 
					LEFT JOIN [Role] r ON ri.Id = r.Id
					WHERE r.Id IS NULL
						AND r.IsActive = 1)
			THROW 50000, N'Book not found', 1;

		BEGIN TRAN

			DELETE [UserBook]
			WHERE UserId = @UserId

			INSERT INTO [UserBook] (UserId, BookId)
			SELECT DISTINCT @UserId, bi.Id 
			FROM @BookIds bi

		COMMIT

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK;
		THROW;
	END CATCH
END