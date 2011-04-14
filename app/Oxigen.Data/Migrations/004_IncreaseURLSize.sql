/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Slides
	DROP CONSTRAINT FK_Slides_SlideFolders
GO
ALTER TABLE dbo.SlideFolders SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Slides
	DROP CONSTRAINT DF_Slides_AddDate
GO
ALTER TABLE dbo.Slides
	DROP CONSTRAINT DF_Slides_EditDate
GO
CREATE TABLE dbo.Tmp_Slides
	(
	SLIDE_ID int NOT NULL IDENTITY (1, 1),
	SlideFolderID int NOT NULL,
	Filename nvarchar(255) NOT NULL,
	FilenameExtension nvarchar(10) NULL,
	FilenameNoPath nvarchar(255) NOT NULL,
	GUID nvarchar(50) NOT NULL,
	SubDir char(1) NOT NULL,
	SlideName nvarchar(50) NOT NULL,
	Creator nvarchar(100) NULL,
	Caption nvarchar(400) NULL,
	ClickThroughURL nvarchar(500) NULL,
	WebsiteURL nvarchar(255) NULL,
	DisplayDuration float(53) NOT NULL,
	Length int NOT NULL,
	ImagePath nvarchar(255) NULL,
	ImagePathWinFS nvarchar(255) NULL,
	ImageFilename nvarchar(255) NULL,
	PlayerType nchar(20) NOT NULL,
	PreviewType nvarchar(10) NOT NULL,
	bLocked bit NOT NULL,
	UserGivenDate datetime NULL,
	AddDate datetime NOT NULL,
	EditDate datetime NOT NULL,
	MadeDirtyLastDate datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Slides SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Slides ADD CONSTRAINT
	DF_Slides_AddDate DEFAULT (getdate()) FOR AddDate
GO
ALTER TABLE dbo.Tmp_Slides ADD CONSTRAINT
	DF_Slides_EditDate DEFAULT (getdate()) FOR EditDate
GO
SET IDENTITY_INSERT dbo.Tmp_Slides ON
GO
IF EXISTS(SELECT * FROM dbo.Slides)
	 EXEC('INSERT INTO dbo.Tmp_Slides (SLIDE_ID, SlideFolderID, Filename, FilenameExtension, FilenameNoPath, GUID, SubDir, SlideName, Creator, Caption, ClickThroughURL, WebsiteURL, DisplayDuration, Length, ImagePath, ImagePathWinFS, ImageFilename, PlayerType, PreviewType, bLocked, UserGivenDate, AddDate, EditDate, MadeDirtyLastDate)
		SELECT SLIDE_ID, SlideFolderID, Filename, FilenameExtension, FilenameNoPath, GUID, SubDir, SlideName, Creator, Caption, ClickThroughURL, WebsiteURL, DisplayDuration, Length, ImagePath, ImagePathWinFS, ImageFilename, PlayerType, PreviewType, bLocked, UserGivenDate, AddDate, EditDate, MadeDirtyLastDate FROM dbo.Slides WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Slides OFF
GO
ALTER TABLE dbo.ChannelsSlides
	DROP CONSTRAINT FK_ChannelsSlides_Slides
GO
DROP TABLE dbo.Slides
GO
EXECUTE sp_rename N'dbo.Tmp_Slides', N'Slides', 'OBJECT' 
GO
ALTER TABLE dbo.Slides ADD CONSTRAINT
	PK_Slides PRIMARY KEY CLUSTERED 
	(
	SLIDE_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Slides ADD CONSTRAINT
	FK_Slides_SlideFolders FOREIGN KEY
	(
	SlideFolderID
	) REFERENCES dbo.SlideFolders
	(
	SLIDEFOLDER_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ChannelsSlides ADD CONSTRAINT
	FK_ChannelsSlides_Slides FOREIGN KEY
	(
	SlideID
	) REFERENCES dbo.Slides
	(
	SLIDE_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ChannelsSlides SET (LOCK_ESCALATION = TABLE)
GO
COMMIT


BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ChannelsSlides
	DROP CONSTRAINT FK_ChannelsSlides_Slides
GO
ALTER TABLE dbo.Slides SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.ChannelsSlides
	DROP CONSTRAINT FK_ChannelsSlides_Channels
GO
ALTER TABLE dbo.Channels SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ChannelsSlides
	(
	CHANNELSSLIDE_ID int NOT NULL IDENTITY (1, 1),
	ChannelID int NOT NULL,
	SlideID int NOT NULL,
	ClickThroughURL nvarchar(500) NULL,
	DisplayDuration float(53) NOT NULL,
	Schedule nvarchar(MAX) NULL,
	PresentationConvertedSchedule nvarchar(4000) NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ChannelsSlides SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_ChannelsSlides ON
GO
IF EXISTS(SELECT * FROM dbo.ChannelsSlides)
	 EXEC('INSERT INTO dbo.Tmp_ChannelsSlides (CHANNELSSLIDE_ID, ChannelID, SlideID, ClickThroughURL, DisplayDuration, Schedule, PresentationConvertedSchedule)
		SELECT CHANNELSSLIDE_ID, ChannelID, SlideID, ClickThroughURL, DisplayDuration, Schedule, PresentationConvertedSchedule FROM dbo.ChannelsSlides WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_ChannelsSlides OFF
GO
DROP TABLE dbo.ChannelsSlides
GO
EXECUTE sp_rename N'dbo.Tmp_ChannelsSlides', N'ChannelsSlides', 'OBJECT' 
GO
ALTER TABLE dbo.ChannelsSlides ADD CONSTRAINT
	PK_ChannelsSlides_1 PRIMARY KEY NONCLUSTERED 
	(
	CHANNELSSLIDE_ID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE CLUSTERED INDEX IX_ChannelsSlides ON dbo.ChannelsSlides
	(
	ChannelID,
	SlideID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.ChannelsSlides ADD CONSTRAINT
	FK_ChannelsSlides_Channels FOREIGN KEY
	(
	ChannelID
	) REFERENCES dbo.Channels
	(
	CHANNEL_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ChannelsSlides ADD CONSTRAINT
	FK_ChannelsSlides_Slides FOREIGN KEY
	(
	SlideID
	) REFERENCES dbo.Slides
	(
	SLIDE_ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
