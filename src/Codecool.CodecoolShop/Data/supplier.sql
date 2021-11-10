USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[supplier]    Script Date: 10/11/2021 11:55:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[supplier]') AND type in (N'U'))
DROP TABLE [dbo].[supplier]
GO

/****** Object:  Table [dbo].[supplier]    Script Date: 10/11/2021 11:55:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[supplier](
	[ID] [int] NOT NULL,
	[name] [text] NOT NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

