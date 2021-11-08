USE [CodeCoolShop]
GO

/****** Object:  Table [dbo].[product]    Script Date: 11/8/2021 2:36:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[product]') AND type in (N'U'))
DROP TABLE [dbo].[product]
GO

/****** Object:  Table [dbo].[product]    Script Date: 11/8/2021 2:36:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [text] NOT NULL,
	[supplier] [text] NOT NULL,
	[description] [text] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[category] [text] NOT NULL,
	[currency] [text] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

INSERT INTO [dbo].[product](
name, supplier, description, price, category, currency)
VALUES ('Amazon Fire','Amazon', 'Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.', 49.99,
'Tablet', 'USD'),
('Lenovo IdeaPad Miix 700','Lenovo', 'Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.', 479.99,
'Laptop', 'USD'),
('Amazon Fire HD 8','Amazon','Amazons latest Fire HD 8 tablet is a great value for media consumption.', 89.99,
'Tablet', 'USD'),
('Lenovo Tab M10 Plus', 'Lenovo', 'Premium look & feel with metal back cover and slim, narrow bezels. Enjoy your favorite videos on the 10.3 FHD display with TDDI technology.', 199.99,
'Tablet', 'USD');
GO


