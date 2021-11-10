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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Lenovo IdeaPad Miix 700',
           2,
           'Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.'
           ,479.0
           ,2
           ,'USD')
GO

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Amazon Fire HD 8',
           1,
           'Amazons latest Fire HD 8 tablet is a great value for media consumption.'
           ,89.0
           ,1
           ,'USD')
GO

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Lenovo Tab M10 Plus',
           1,
           'Premium look & feel with metal back cover and slim, narrow bezels. Enjoy your favorite videos on the 10.3 FHD display with TDDI technology'
           ,199.0
           ,2
           ,'USD')
GO

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Asus ROG Strix G15',
           2,
           'Professional gaming device. Long live PC Master Race. If its expensive it must be good'
           ,2899.0
           ,3
           ,'USD')
GO

INSERT INTO [dbo].[product]
           ([name]
           ,[supplier_id]
           ,[description]
           ,[price]
           ,[category_id]
           ,[currency])
     VALUES
           ('Asus ZenPad',
           1,
           'Elegant, embossed knit patterning gives Asus ZenPad 10 refined, luxurious styling that fits any setting.'
           ,269.0
           ,3
           ,'USD')
GO