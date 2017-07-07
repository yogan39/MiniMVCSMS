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

