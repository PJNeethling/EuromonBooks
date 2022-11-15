CREATE OR ALTER PROC UpsertUser
	@Uuid UNIQUEIDENTIFIER,
	@UserName NVARCHAR(50),
	@FirstName NVARCHAR(100),
	@LastName NVARCHAR(100),
	@Email NVARCHAR(100),
	@Number NVARCHAR(20),
	@Password NVARCHAR(255),
	@Passphrase VARCHAR(128),
	@StatusId INT
AS
BEGIN
	BEGIN TRY
		DECLARE @UserId INT, @InternalUserName NVARCHAR(50), @InternalEmail NVARCHAR(100)

		SELECT TOP (1) @UserId = u.Id
		FROM [User] u
		WHERE u.Uuid = @Uuid
		
		IF @Uuid IS NOT NULL AND @UserId IS NULL
			THROW 50000, N'User not found', 1;

		IF @StatusId IS NOT NULL AND NOT EXISTS(SELECT 1 FROM Status st WHERE Id = @StatusId)
			THROW 50000, N'Status not found', 1;

		SELECT @InternalUserName = u.Username, @InternalEmail = u.Email 
		FROM [User] u
		WHERE u.Id = @UserId
			AND @Uuid <> u.Uuid
			AND (u.Email = @Email
			OR u.Username = @UserName)

			IF @InternalUserName IS NOT NULL
			THROW 50001, N'Username already exists', 1;

			IF @InternalEmail IS NOT NULL
			THROW 50001, N'Email already exists', 1;
				
		DECLARE @output TABLE
		(
			InsertedUuid UNIQUEIDENTIFIER NULL
		);

		BEGIN TRAN

			MERGE [User] AS tar
			USING
			(
				VALUES (ISNULL(@Uuid, NEWID()), @UserName, @Password, @FirstName, @LastName, @Email, @Number, ISNULL(@StatusId, 0))
			) src (Uuid, Username, [Password], FirstName, LastName, Email, Number, StatusId)
			ON tar.Uuid = src.Uuid
			WHEN MATCHED AND (tar.Username <> src.Username
							OR ISNULL(tar.FirstName, '') <> ISNULL(src.FirstName, '')
							OR ISNULL(tar.LastName, '') <> ISNULL(src.LastName, '')
							OR tar.Email <> src.Email
							OR ISNULL(tar.Number, '') <> ISNULL(src.Number, '')
							OR tar.StatusId <> src.StatusId)
				THEN UPDATE
					SET Username = ENCRYPTBYPASSPHRASE(@Passphrase, src.Username),
					[Password] = ISNULL(src.[Password], tar.[Password]),
					FirstName = ENCRYPTBYPASSPHRASE(@Passphrase, src.FirstName),
					LastName = ENCRYPTBYPASSPHRASE(@Passphrase, src.LastName),
					Email = ENCRYPTBYPASSPHRASE(@Passphrase, src.Email),
					Number = ENCRYPTBYPASSPHRASE(@Passphrase, src.Number),
					StatusId = src.StatusId,
					ModifiedDate = GETUTCDATE()
			WHEN NOT MATCHED
			THEN INSERT (Username, [Password], FirstName, LastName, Email, Number, StatusId)
					VALUES (ENCRYPTBYPASSPHRASE(@Passphrase, src.Username), src.[Password], ENCRYPTBYPASSPHRASE(@Passphrase, src.FirstName), ENCRYPTBYPASSPHRASE(@Passphrase, src.LastName), ENCRYPTBYPASSPHRASE(@Passphrase, src.Email), ENCRYPTBYPASSPHRASE(@Passphrase, src.Number), src.StatusId)
			OUTPUT Inserted.Uuid INTO @output;

		COMMIT

		SELECT InsertedUuid AS Uuid
		FROM @output

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 
			ROLLBACK;
		THROW;
	END CATCH
END