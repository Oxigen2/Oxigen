
CREATE TABLE [dbo].[RSSFeeds](
	[RSSFEED_ID] [int] IDENTITY(1,1) NOT NULL,
	[PublisherID] [int] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[ChannelID] [int] NOT NULL,
	[SlideFolderID] [int] NOT NULL,
	[Name] [nvarchar] (100) NOT NULL,
	[URL] [nvarchar](1000) NOT NULL,
	[XSLT] [nvarchar](2000) NOT NULL,
	[LastItem] [nvarchar](1000) NULL,
	[LastChecked] [datetime] NULL,
 CONSTRAINT [PK_RSSFeed] PRIMARY KEY CLUSTERED 
(
	[RSSFEED_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
