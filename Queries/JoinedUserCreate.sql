USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[JoinedUser]    Script Date: 05/12/2012 07:12:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JoinedUser]') AND type in (N'U'))
DROP TABLE [dbo].[JoinedUser]
GO

USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[JoinedUser]    Script Date: 05/12/2012 13:49:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[JoinedUser](
	[UserId] [int] NOT NULL,
	[SuggestionId] [int] NOT NULL,
	[Weekdays] [varchar](50) NOT NULL,
 CONSTRAINT [PK_JoinedUser_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[SuggestionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[JoinedUser]  WITH CHECK ADD  CONSTRAINT [FK_JoinedUser_Suggestion] FOREIGN KEY([SuggestionId])
REFERENCES [dbo].[Suggestion] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[JoinedUser] CHECK CONSTRAINT [FK_JoinedUser_Suggestion]
GO

