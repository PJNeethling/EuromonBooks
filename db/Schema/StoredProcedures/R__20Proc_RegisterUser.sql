CREATE OR ALTER PROC RegisterUser
	@UserName NVARCHAR(50),
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@Email NVARCHAR(100),
	@Password NVARCHAR(255),
	@Passphrase VARCHAR(128)
AS
BEGIN
	BEGIN TRY
		DECLARE @UserId INT, @InternalUserName NVARCHAR(50), @InternalEmail NVARCHAR(100)

		SELECT @InternalUserName = u.Username
		FROM [User] u
		WHERE CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.UserName) AS nvarchar(max)) = @UserName

		IF @InternalUserName IS NOT NULL
		THROW 50001, N'Username already exists', 1;
				
		DECLARE @output TABLE
		(
			InsertedId INT,
			InsertedUuid UNIQUEIDENTIFIER NULL
		);

		BEGIN TRAN

			MERGE [User] AS tar
			USING
			(
				VALUES (NEWID(), @UserName, @Password, @FirstName, @LastName, @Email)
			) src (Uuid, Username, [Password], FirstName, LastName, Email)
			ON tar.Uuid = src.Uuid
			WHEN NOT MATCHED
			THEN INSERT (Username, [Password], FirstName, LastName, Email, StatusId)
					VALUES (ENCRYPTBYPASSPHRASE(@Passphrase, src.Username), src.[Password], ENCRYPTBYPASSPHRASE(@Passphrase, src.FirstName), ENCRYPTBYPASSPHRASE(@Passphrase, src.LastName), ENCRYPTBYPASSPHRASE(@Passphrase, src.Email), 1)
			OUTPUT Inserted.Id,Inserted.Uuid INTO @output;

			INSERT INTO UserRole (UserId, RoleId)
			SELECT InsertedId, 2
			FROM @output

		COMMIT

		SELECT InsertedUuid AS Uuid
		FROM @output

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK;
		IF (ERROR_MESSAGE() LIKE '%FK_User_Status%')
			THROW 50000, 'Status not found', 1;
		ELSE
			THROW;
		THROW;
	END CATCH
END