
CREATE TABLE [dbo].[Templates](
	[TEMPLATE_ID] [int] IDENTITY(1,1) NOT NULL,
	[OwnedByID] [int] NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[Filename] [nvarchar](255) NOT NULL,
	[FilenameNoPath] [nvarchar](255) NOT NULL,
	[FilenameExtension] [nvarchar](10) NOT NULL,
	[ImagePath] [nvarchar](255) NOT NULL,
	[ImagePathWinFS] [nvarchar](255) NOT NULL,
	[SubDir] [char](1) NULL,
	[ImageName] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[MetaData] [nvarchar](1000) NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TEMPLATE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

