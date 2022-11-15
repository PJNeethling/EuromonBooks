CREATE OR ALTER PROC AssignRolesToUser
	@Uuid UNIQUEIDENTIFIER,
	@RoleIds IdListType READONLY
AS
BEGIN
	BEGIN TRY
		DECLARE @UserId INT

		SELECT @UserId = u.Id 
		FROM [User] u
		WHERE u.Uuid = @Uuid


		IF @UserId IS NULL
			THROW 50000, N'User not found', 1;

		IF EXISTS (SELECT * FROM @RoleIds ri 
					LEFT JOIN [Role] r ON ri.Id = r.Id
					WHERE r.Id IS NULL)
			THROW 50000, N'Role not found', 1;

		BEGIN TRAN

			DELETE [UserRole]
			WHERE UserId = @UserId

			INSERT INTO [UserRole] (UserId, RoleId)
			SELECT DISTINCT @UserId, ri.Id FROM @RoleIds ri

		COMMIT

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK;
		THROW;
	END CATCH
END