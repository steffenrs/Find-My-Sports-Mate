USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[Sport]    Script Date: 05/12/2012 07:13:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sport]') AND type in (N'U'))
DROP TABLE [dbo].[Sport]
GO

USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[Sport]    Script Date: 05/09/2012 09:39:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Sport](
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Sport] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

