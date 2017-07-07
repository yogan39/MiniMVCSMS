IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MinTable]') AND type in (N'U'))
DROP TABLE [dbo].[MinTable]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MinTable]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MinTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MobileNos] [nvarchar](max) NULL,
	[SentDate] [datetime] NULL,
	[MessageSent] [nvarchar](200) NULL,
	[GroupName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
