CREATE OR ALTER PROC GetAllBooksForUser
	@Uuid UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @UserId INT

	SELECT @UserId = u.Id 
	FROM [User] u
	WHERE u.Uuid = @Uuid

	IF @UserId IS NULL
		THROW 50000, N'User not found', 1;

	;WITH BooksCTE AS
	(
		SELECT b.Id,
			b.[Name],
			b.[Description],
			b.[Text],
			b.[PurchasePrice],
			b.[IsActive],
			b.CreatedDate,
			b.ModifiedDate
		FROM [Book] b
		INNER JOIN UserBook ub ON b.Id = ub.BookId
		WHERE ub.UserId = @UserId
	)

	SELECT *
	FROM (SELECT COUNT(1) TotalItems FROM BooksCTE) AS TotalItems
	LEFT JOIN (select *
		FROM BooksCTE) AS CTE ON 1=1;
END