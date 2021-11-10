USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[supplier]    Script Date: 10/11/2021 1:58:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[supplier]') AND type in (N'U'))
DROP TABLE [dbo].[supplier]
GO

/****** Object:  Table [dbo].[supplier]    Script Date: 10/11/2021 1:58:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [CodeCoolShop]
GO

INSERT INTO [dbo].[supplier]
           ([name]
           ,[description])
     VALUES
           ('Amazon'
           ,'Digital content and services')
GO

INSERT INTO [dbo].[supplier]
           ([name]
           ,[description])
     VALUES
           ('Lenovo'
           ,'Computers')
GO

INSERT INTO [dbo].[supplier]
           ([name]
           ,[description])
     VALUES
           ('Asus'
           ,'Computers')
GO


