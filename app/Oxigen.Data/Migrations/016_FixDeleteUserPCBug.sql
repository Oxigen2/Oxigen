ALTER TABLE PCs ADD DeleteDate datetime;
GO
ALTER TABLE Users ADD DeleteDate datetime;
GO


/****** Object:  StoredProcedure [dbo].[dp_removePCFromUninstall]    Script Date: 05/20/2011 17:55:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_removePCFromUninstall]

(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

DECLARE @UserID int;
DECLARE @PcID int;

SELECT
	@PcID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;
	
SELECT
	@UserID = Users.USER_ID
FROM
	Users
WHERE
	Users.UserGUID = @UserGUID;

IF EXISTS (SELECT 1 FROM PCs 
				INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				WHERE PCs.PC_ID = @PcID 
				AND PCs.bDeleted = 0
				AND Consumers.UserID = @UserID)
BEGIN

	UPDATE
		Channels
	SET
		NoFollowers = NoFollowers - 1
	FROM
		Channels INNER JOIN PcChannels ON Channels.CHANNEL_ID = PcChannels.ChannelID
	WHERE
		PcChannels.PcID = @PcID;

	UPDATE 
		PCs
	SET 
		PCs.bDeleted = 1,
		PCs.DeleteDate = GetDate()
	WHERE 
		PCs.PC_ID = @PcID;

END

GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_removeUserAccount]

(
@UserID int
)

AS

DELETE 
	PcChannels
FROM
	PcChannels INNER JOIN Channels ON PcChannels.ChannelID = Channels.CHANNEL_ID
	INNER JOIN Publishers on Publishers.PUBLISHER_ID = Channels.PublisherID
WHERE
	Publishers.UserID = @UserID;
	
DELETE 
	PcChannels
FROM
	PcChannels INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
	INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;
	
DELETE
	ChannelPasswordUnlock
FROM
	ChannelPasswordUnlock INNER JOIN Channels ON ChannelPasswordUnlock.ChannelID = Channels.CHANNEL_ID
	INNER JOIN Publishers on Publishers.PUBLISHER_ID = Channels.PublisherID
WHERE
	Publishers.UserID = @UserID;

DELETE 
	ChannelsSlides 
FROM
	ChannelsSlides INNER JOIN Channels ON Channels.CHANNEL_ID = ChannelsSlides.ChannelID
	INNER JOIN Publishers on Publishers.PUBLISHER_ID = Channels.PublisherID
WHERE
	Publishers.UserID = @UserID;	

DELETE
	Channels
FROM
	Channels INNER JOIN Publishers on Publishers.PUBLISHER_ID = Channels.PublisherID
WHERE
	Publishers.UserID = @UserID;
	
DELETE
	Slides
FROM
	Slides INNER JOIN SlideFolders on Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;
	
DELETE
	AssetContents
FROM
	AssetContents INNER JOIN AssetContentFolders on AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;

DELETE 
	AssetContentFolders
FROM
	AssetContentFolders INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;
	
DELETE
	SlideFolders
FROM
	SlideFolders INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;

DELETE FROM
	ChannelPasswordUnlock
WHERE
	ChannelPasswordUnlock.UserID = @UserID;
	
DELETE
	PCVersionHistory
FROM
	PCVersionHistory INNER JOIN PCs ON PCs.PC_ID = PCVersionHistory.PcID
	INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;
	
-- This step is redundant as user should have deleted their PCs (meaning removed their software) before being allowed to delete their account. --
UPDATE 
	PCs
SET
	PCs.bDeleted = 1
FROM 
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;

UPDATE
	Users
SET
	Users.bDeleted = 1,
	Users.DeleteDate = GetDate(),
	Users.Username = Users.UserGUID /*Username must be unique so change this to allow it to be reused*/
WHERE
	Users.USER_ID = @UserID;

GO

