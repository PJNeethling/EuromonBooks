CREATE OR ALTER PROC GetAllBooks
AS
BEGIN

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
	)

	SELECT *
	FROM (SELECT COUNT(1) TotalItems FROM BooksCTE) AS TotalItems
	LEFT JOIN (select *
		FROM BooksCTE) AS CTE ON 1=1;
END