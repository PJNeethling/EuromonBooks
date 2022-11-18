CREATE OR ALTER PROC UserLogin
	@EmailOrUsername NVARCHAR(255),
	@Passphrase VARCHAR(128)
AS

BEGIN

	SELECT u.Uuid, u.[Password],
	(
		SELECT 
		(
			SELECT ur.RoleId AS Id
			FROM UserRole ur
			WHERE ur.UserId = u.Id
			FOR JSON PATH
		)
	) Roles
	FROM [User] u 
	WHERE (CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Username) AS VARCHAR(1000)) = @EmailOrUsername 
		OR CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Email) AS VARCHAR(1000)) = @EmailOrUsername)
	AND u.StatusId = 1

END