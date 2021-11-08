USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[user]    Script Date: 11/8/2021 2:15:37 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
DROP TABLE [dbo].[user]
GO

/****** Object:  Table [dbo].[user]    Script Date: 11/8/2021 2:15:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[password] [text] NOT NULL,
	[email] [text] NOT NULL,
	[phone_number] [text] NOT NULL,
	[address] [text] NOT NULL,
	[city] [text] NOT NULL,
	[country] [text] NOT NULL,
	[zipcode] [text] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


