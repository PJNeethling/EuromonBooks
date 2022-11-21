CREATE OR ALTER PROC PurchaseUserBook
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

		IF NOT EXISTS (SELECT * FROM Book b 
					WHERE b.Id = @BookId
						AND b.IsActive = 1)
			THROW 50000, N'Book not found', 1;

		IF EXISTS (SELECT * FROM UserBook ub 
					WHERE ub.BookId = @BookId
						AND ub.UserId = @UserId)
			THROW 50000, N'Book already purchased', 1;

		BEGIN TRAN

			INSERT INTO [UserBook] (UserId, BookId)
			SELECT @UserId, @BookId

		COMMIT

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK;
		THROW;
	END CATCH
END