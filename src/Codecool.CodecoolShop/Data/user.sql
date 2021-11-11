USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[user]    Script Date: 11/11/2021 11:41:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
DROP TABLE [dbo].[user]
GO

/****** Object:  Table [dbo].[user]    Script Date: 11/11/2021 11:41:05 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[password] [text] NOT NULL,
	[email] [text] NOT NULL,
	[phone_number] [text] NULL,
	[address] [text] NULL,
	[city] [text] NULL,
	[country] [text] NULL,
	[zipcode] [text] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [CodeCoolShop]
GO

INSERT INTO [dbo].[user]
           ([name]
           ,[password]
           ,[email]
           ,[phone_number]
           ,[address]
           ,[city]
           ,[country]
           ,[zipcode])
     VALUES
           ('Maricica'
           ,'zurgalai'
           ,'maricica@yahoo.com'
           ,'0240541000'
           ,'Str. lalea'
           ,'Zalau'
           ,'Romania'
           ,'1234')
GO


