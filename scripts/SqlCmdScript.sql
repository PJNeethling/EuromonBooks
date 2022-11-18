IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'EuromonBooks')
  CREATE database [EuromonBooks];
GO

-- Create the default dB user
USE [master]
GO

IF NOT EXISTS(SELECT 1 FROM [EuromonBooks].sys.database_principals WHERE name = 'euromon')
  CREATE LOGIN [euromon] WITH PASSWORD=N'!Q@W3e4r', DEFAULT_DATABASE=[EuromonBooks], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [EuromonBooks]
GO

IF NOT EXISTS(SELECT 1 FROM [EuromonBooks].sys.database_principals WHERE name = 'euromon')
BEGIN
  CREATE USER [euromon] FOR LOGIN [euromon]
  ALTER USER [euromon] WITH DEFAULT_SCHEMA=[dbo]
  ALTER ROLE [db_owner] ADD MEMBER [euromon]
END
GO