USE [FindMySportsMate]
GO

/****** Object:  Table [dbo].[Suggestion]    Script Date: 05/02/2012 13:56:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Suggestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SportId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[IsClosed] [bit] NOT NULL,
	[MinimumUsers] [int] NOT NULL,
	[MaximumUsers] [int] NOT NULL,
 CONSTRAINT [PK_Suggestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

