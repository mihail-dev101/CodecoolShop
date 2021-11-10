USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[category]    Script Date: 10/11/2021 1:53:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[category]') AND type in (N'U'))
DROP TABLE [dbo].[category]
GO

/****** Object:  Table [dbo].[category]    Script Date: 10/11/2021 1:53:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[description] [text] NULL,
	[department] [text] NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [CodeCoolShop]
GO

INSERT INTO [dbo].[category]
           ([name]
           ,[description]
           ,[department])
     VALUES
           ('Tablet'
           ,'A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display.'
           ,'Hardware')
GO

INSERT INTO [dbo].[category]
           ([name]
           ,[description]
           ,[department])
     VALUES
           ('Laptop'
           ,'Laptop'
           ,'Hardware')
GO


