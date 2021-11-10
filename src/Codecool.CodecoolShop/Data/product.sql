USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[product]    Script Date: 10/11/2021 1:50:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[product]') AND type in (N'U'))
DROP TABLE [dbo].[product]
GO

/****** Object:  Table [dbo].[product]    Script Date: 10/11/2021 1:50:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[supplier_id] [int] NOT NULL,
	[description] [text] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[category_id] [int] NOT NULL,
	[currency] [text] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Amazon Fire',
           1,
           'Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.'
           ,49.9
           ,1
           ,'USD')
GO
