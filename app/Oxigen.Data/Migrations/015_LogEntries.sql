

CREATE TABLE [dbo].[LogEntries](
	[LOGENTRY_ID] [int] NOT NULL,
	[LogName] [varchar](50) NOT NULL,
	[UserRef] [varchar](100) NOT NULL,
	[Message] [varchar](100) NOT NULL,
	[IpAddress] [varchar](100) NOT NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LogEntries] PRIMARY KEY CLUSTERED 
(
	[LOGENTRY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]




