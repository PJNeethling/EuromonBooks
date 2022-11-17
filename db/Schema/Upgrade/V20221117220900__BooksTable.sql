IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Text] [nvarchar](250) NOT NULL,
	[PurchasePrice] DECIMAL(5,2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'DF_Book_IsActive') AND type in (N'D'))
	ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'DF_Book_CreatedDate') AND type in (N'D'))
	ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_CreatedDate]  DEFAULT (GETUTCDATE()) FOR [CreatedDate]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserBook]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[UserBook](
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	CONSTRAINT [PK_UserBook] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC,
		[BookId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserBook_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserBook]'))
   ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_User] FOREIGN KEY([UserId])
	REFERENCES [dbo].[User] ([Id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserBook_Book]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserBook]'))
   ALTER TABLE [dbo].[UserBook]  WITH CHECK ADD  CONSTRAINT [FK_UserBook_Book] FOREIGN KEY([BookId])
	REFERENCES [dbo].[Book] ([Id])
GO
