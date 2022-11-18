CREATE OR ALTER PROC GetAllUsers
	@StatusId INT,
	@Passphrase VARCHAR(128)
AS
BEGIN

	;WITH UsersCTE AS
	(
		SELECT u.Uuid,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.FirstName) AS NVARCHAR(MAX)) FirstName,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.LastName) AS NVARCHAR(MAX)) LastName,
			CAST(DECRYPTBYPASSPHRASE(@Passphrase, u.Email) AS NVARCHAR(MAX)) Email,
			u.StatusId,
			u.CreatedDate,
			u.ModifiedDate
		FROM [User] u
		INNER JOIN [Status] s On u.StatusId = s.Id
		WHERE u.StatusId = ISNULL(@StatusId, u.StatusId)
	)

	SELECT *
	FROM (SELECT COUNT(1) TotalItems FROM UsersCTE) AS TotalItems
	LEFT JOIN (select *
		FROM UsersCTE) AS CTE ON 1=1;


END