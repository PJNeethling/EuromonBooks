IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Status](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [varchar](20) NOT NULL,
	 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[User](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Uuid] [uniqueidentifier] NOT NULL,
		[UserName] [nvarchar](50) NULL,
		[Password] [nvarchar](255) NOT NULL,
		[FirstName] [nvarchar](100) NULL,
		[LastName] [nvarchar](100) NULL,
		[CreatedDate] [datetime] NOT NULL,
		[ModifiedDate] [datetime] NULL,
		[Email] [nvarchar](100) NOT NULL,
		[Number] [nvarchar](20) NULL,
		[StatusId] [int] NOT NULL
	 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'DF_User_Uuid') AND type in (N'D'))
    ALTER TABLE [dbo].[User] ADD CONSTRAINT [DF_User_Uuid] DEFAULT (newid()) FOR [Uuid]
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'DF_User_CreatedDate') AND type in (N'D'))
    ALTER TABLE [dbo].[User] ADD CONSTRAINT [DF_User_CreatedDate] DEFAULT (getutcdate()) FOR [CreatedDate]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_User_Status]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
   ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Status] FOREIGN KEY([StatusId])
	REFERENCES [dbo].[Status] ([Id])
GO
