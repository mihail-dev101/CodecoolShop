USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[cart]    Script Date: 11/8/2021 2:13:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cart]') AND type in (N'U'))
DROP TABLE [dbo].[cart]
GO

/****** Object:  Table [dbo].[cart]    Script Date: 11/8/2021 2:13:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cart](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [text] NULL,
	[user_id] [int] NULL,
 CONSTRAINT [PK_cart] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


