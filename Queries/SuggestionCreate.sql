USE [n8487171]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Suggestion_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Suggestion]'))
ALTER TABLE [dbo].[Suggestion] DROP CONSTRAINT [FK_Suggestion_User]
GO

USE [n8487171]
GO

/****** Object:  Table [dbo].[Suggestion]    Script Date: 05/22/2012 05:43:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suggestion]') AND type in (N'U'))
DROP TABLE [dbo].[Suggestion]
GO

USE [n8487171]
GO

/****** Object:  Table [dbo].[Suggestion]    Script Date: 05/22/2012 05:43:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Suggestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SportId] [varchar](50) NOT NULL,
	[LocationId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[IsClosed] [bit] NOT NULL,
	[MinimumUsers] [int] NOT NULL,
	[MaximumUsers] [int] NOT NULL,
	[MostPopularDays] [varchar](50) NULL,
 CONSTRAINT [PK_Suggestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Suggestion]  WITH CHECK ADD  CONSTRAINT [FK_Suggestion_User] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Suggestion] CHECK CONSTRAINT [FK_Suggestion_User]
GO


