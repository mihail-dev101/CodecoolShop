USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[order]    Script Date: 11/8/2021 2:15:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[order]') AND type in (N'U'))
DROP TABLE [dbo].[order]
GO

/****** Object:  Table [dbo].[order]    Script Date: 11/8/2021 2:15:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[ordered_products] [text] NOT NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


