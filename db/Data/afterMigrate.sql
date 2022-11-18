
BEGIN TRAN

SET IDENTITY_INSERT [User] ON

MERGE [User] AS tar
USING (VALUES 	(1, N'pjneethling', N'A$s2$16$16$2$osXK9AyRzBCf+UpNs6ZM6kGP1g62/Tu1qvIdvXN1BMc=$/kgA0fXBtuDhRrEvUtVf6xxAK7Dg5LUKyTv2y16yD8s=', N'PJ', N'Neethling', 1, N'neethlingpeterjohn@gmail.com', CAST(NULL AS NVARCHAR(15))),
				(2, N'guestUsername', N'A$s2$16$16$2$osXK9AyRzBCf+UpNs6ZM6kGP1g62/Tu1qvIdvXN1BMc=$/kgA0fXBtuDhRrEvUtVf6xxAK7Dg5LUKyTv2y16yD8s=', N'Guest', N'User', 1, N'guest@mail.com', CAST(NULL AS NVARCHAR(15)))
				
	) src (Id, Username, Password, FirstName, LastName, StatusId, Email, Number)
	ON tar.Id = src.Id
WHEN MATCHED THEN
	UPDATE SET
		Username = ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.Username),
		Password = src.Password,
		FirstName = ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.FirstName),
		LastName = ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.LastName),
		StatusId = src.StatusId,
		Email = ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.Email),
		Number = ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.Number)
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Username, Password, FirstName, LastName, StatusId, Email, Number)
	VALUES (src.Id, ENCRYPTBYPASSPHRASE('BWM8DMLALrFH2bL02Cgq', src.Username), src.Password, ENCRYPTBYPASSPHRASE('GXM8RPDELrFM5bX02Cnj', src.FirstName), ENCRYPTBYPASSPHRASE('GXM8RPDELrFM5bX02Cnj', src.LastName), src.StatusId, ENCRYPTBYPASSPHRASE('GXM8RPDELrFM5bX02Cnj', src.Email), ENCRYPTBYPASSPHRASE('GXM8RPDELrFM5bX02Cnj', src.Number));

SET IDENTITY_INSERT [User] OFF

COMMIT
GO

DECLARE @maxId INT
SELECT @maxId = ISNULL(MAX(Id), 1) FROM [User]
DBCC CHECKIDENT ('dbo.User', RESEED, @maxId) WITH NO_INFOMSGS
GO

BEGIN TRAN

SET IDENTITY_INSERT [Role] ON

MERGE [Role] AS tar
USING (VALUES	(1, 'Admin', 1, 'Admin role which gives access to most features within the system for EuromonBook system.'),
				(2, 'Default User', 1, 'Default User role within the EuromonBook system.')
	) src (Id, Name, IsActive, Description)
	ON tar.Id = src.Id
WHEN MATCHED THEN
	UPDATE SET
		Name = src.Name,
		IsActive = src.IsActive,
		Description = src.Description,
		ModifiedDate = GETUTCDATE()
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Name, IsActive, Description, CreatedDate)
	VALUES (Id, Name, IsActive, Description, GETUTCDATE());

SET IDENTITY_INSERT [Role] OFF

COMMIT
GO


DECLARE @maxId INT
SELECT @maxId = ISNULL(MAX(Id), 1) FROM [Role]
DBCC CHECKIDENT ('dbo.Role', RESEED, @maxId) WITH NO_INFOMSGS
GO

BEGIN TRAN

DECLARE @UR TABLE 
(
	UserId INT,
	RoleId INT
)

INSERT INTO @UR
SELECT 1, Id FROM [Role]
UNION
SELECT 2, Id FROM [Role] WHERE Name = 'Default User'


MERGE UserRole AS tar
USING (SELECT * FROM @UR) src 
	ON tar.UserId = src.UserId AND tar.RoleId = src.RoleId
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UserId, RoleId)
	VALUES (src.UserId, src.RoleId)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;

COMMIT
GO

BEGIN TRAN

SET IDENTITY_INSERT [Book] ON

MERGE [Book] AS tar
USING (VALUES	(1, 'Novel Crumbs', 'Novel Crumbs Description', 'I will eat bread', CAST(44.21 AS DECIMAL(5, 2)), 1),
				(2, 'Author Nerds', 'Author Nerds Description', 'Write my name', CAST(100.00 AS DECIMAL(5, 2)), 1),
				(3, 'Escape the Commander', 'Escape the Commander Description', 'Run forest', CAST(12.23 AS DECIMAL(5, 2)), 1),
				(4, 'Cuddle Reading', 'Cuddle Reading Description', 'Wroem wroem racing', CAST(124.20 AS DECIMAL(5, 2)), 1),
				(5, 'Robot of Destruction', 'Robot of Destruction Description', 'Beep Boob', CAST(77.77 AS DECIMAL(5, 2)), 1),
				(6, 'Terminal Paperback', 'Terminal Paperback Description', 'I am the terminator', CAST(10.00 AS DECIMAL(5, 2)), 1)
	) src (Id, [Name], [Description], [Text], PurchasePrice, IsActive)
	ON tar.Id = src.Id
WHEN MATCHED THEN
	UPDATE SET
		[Name] = src.[Name],
		[Description] = src.[Description],
		[Text] = src.[Text],
		PurchasePrice = src.PurchasePrice,
		IsActive = src.IsActive,
		ModifiedDate = GETUTCDATE()
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, [Name], [Description], [Text], PurchasePrice, IsActive)
	VALUES (src.Id, src.[Name], src.[Description], src.[Text], src.PurchasePrice, src.IsActive);

SET IDENTITY_INSERT [Book] OFF

COMMIT
GO

DECLARE @maxId INT
SELECT @maxId = ISNULL(MAX(Id), 1) FROM [Book]
DBCC CHECKIDENT ('dbo.Book', RESEED, @maxId) WITH NO_INFOMSGS
GO