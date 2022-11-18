CREATE OR ALTER PROC GetUserDetails
	@Uuid UNIQUEIDENTIFIER,
	@Passphrase VARCHAR(128)
AS
BEGIN

	IF NOT EXISTS (SELECT 1 FROM [User] u
					WHERE u.Uuid = @Uuid)
			THROW 50000, 'User not found', 1;

	SELECT CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.FirstName) AS NVARCHAR(MAX)) FirstName,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.LastName) AS NVARCHAR(MAX)) LastName,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Username) AS NVARCHAR(MAX)) Username,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Email) AS NVARCHAR(MAX)) Email,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Number) AS NVARCHAR(MAX)) Number,
			u.StatusId,
			u.CreatedDate,
			u.ModifiedDate,
			(
				SELECT 
				(
					SELECT r.Id, r.Name
					FROM UserRole ur
					INNER JOIN Role r ON r.Id = ur.RoleId
					WHERE ur.UserId = u.Id
					FOR JSON PATH
				)
			) Roles
	FROM [User] u
	WHERE u.Uuid = @Uuid
END