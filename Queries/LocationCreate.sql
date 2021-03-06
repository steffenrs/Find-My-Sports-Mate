USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 05/11/2012 04:43:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
DROP TABLE [dbo].[Location]
GO

USE [FindMySportsMate]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 05/11/2012 04:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Latitude] [varchar](250) NOT NULL,
	[Longitude] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON
INSERT [dbo].[Location] ([Id], [Name], [Latitude], [Longitude]) VALUES (1, N'Mowbray Park', -27.479111, 153.016634)
INSERT [dbo].[Location] ([Id], [Name], [Latitude], [Longitude]) VALUES (3, N'Botanical Gardens, CBD', -27.47538, 153.030281)
INSERT [dbo].[Location] ([Id], [Name], [Latitude], [Longitude]) VALUES (4, N'Raymond Park', -27.480786, 153.039637)
INSERT [dbo].[Location] ([Id], [Name], [Latitude], [Longitude]) VALUES (5, N'Kelvin Grove', -27.451694, 153.016698)
INSERT [dbo].[Location] ([Id], [Name], [Latitude], [Longitude]) VALUES (7, N'New Farm Park', -27.469649, 153.051653)
SET IDENTITY_INSERT [dbo].[Location] OFF
