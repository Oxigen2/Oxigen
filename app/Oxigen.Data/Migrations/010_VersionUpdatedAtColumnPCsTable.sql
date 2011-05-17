/****** Object:  Table [dbo].[PCVersionHistory]    Script Date: 05/13/2011 16:07:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PCVersionHistory](
	[PCVERSIONHISTORY_ID] [int] IDENTITY(1,1) NOT NULL,
	[PcID] [int] NOT NULL,
	[MajorVersionNumber] [int] NOT NULL,
	[MinorVersionNumber] [int] NOT NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_PCVersionHistory] PRIMARY KEY CLUSTERED 
(
	[PCVERSIONHISTORY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PCVersionHistory]  WITH CHECK ADD  CONSTRAINT [FK_PCVersionHistory_PCs] FOREIGN KEY([PcID])
REFERENCES [dbo].[PCs] ([PC_ID])
GO

ALTER TABLE [dbo].[PCVersionHistory] CHECK CONSTRAINT [FK_PCVersionHistory_PCs]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addMachineVersionInfo]
(
@MachineGUID nvarchar(50),
@VersionUpdatedAt datetime,
@MajorVersionNumber int,
@MinorVersionNumber int,
@MachineLive bit OUTPUT
)

AS

DECLARE @PcID int;

IF NOT EXISTS (SELECT 1 FROM PCs WHERE PCs.PCGUID = @MachineGUID)
BEGIN

	SET @MachineLive = 0;

END
ELSE
BEGIN

	SET @MachineLive = 1;
	
	SELECT
		@PcID = PCs.PC_ID
	FROM
		PCs
	WHERE
		PCs.PCGUID = @MachineGUID;
			
	IF NOT EXISTS (SELECT 1 FROM PCVersionHistory 
			WHERE PCVersionHistory.PcID = @PcID
			AND PCVersionHistory.MajorVersionNumber = @MajorVersionNumber
			AND PCVersionHistory.MinorVersionNumber = @MinorVersionNumber)				
	
		INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
		VALUES (@PcID, @MajorVersionNumber, @MinorVersionNumber, @VersionUpdatedAt)
			
END

GO

/****** Object:  StoredProcedure [dbo].[dp_addPCByGUID]    Script Date: 05/13/2011 16:44:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addPCByGUID]
(
@EmailAddress nvarchar(50),
@MachineGUID nvarchar(50),
@MACAddress nvarchar(20) = NULL,
@PCName nvarchar(100),
@SoftwareMajorVersionNumber int,
@SoftwareMinorVersionNumber int
)

AS

DECLARE @ConsumerID int;
DECLARE @PcID int;

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.Username = @EmailAddress;

INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient)
VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @PCName, 1);

SET @PcID = SCOPE_IDENTITY();

INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE())

GO

/****** Object:  StoredProcedure [dbo].[dp_addPCByUserGUIDMachineGUID]    Script Date: 05/13/2011 17:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addPCByUserGUIDMachineGUID]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@MachineName nvarchar(50),
@MACAddress nvarchar(50) = NULL,
@SoftwareMajorVersionNumber int,
@SoftwareMinorVersionNumber int
)

AS

DECLARE @ConsumerID int;
DECLARE @PcID int;

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.UserGUID = @UserGUID;
	
IF EXISTS (SELECT 1 FROM PCs WHERE PCs.PCGUID = @MachineGUID)
BEGIN

	SELECT
		@PcID = PCs.PC_ID
	FROM
		PCs
	WHERE 
		PCs.PCGUID = @MachineGUID;
	
	UPDATE 
		PCs
	SET
		PcName = @MachineName,
		bLinkedToClient = 1
	FROM
		PCs
	WHERE 
		PCs.PC_ID = @PcID;
		
	IF NOT EXISTS (SELECT 1 FROM PCVersionHistory 
			WHERE PCVersionHistory.PcID = @PcID
			AND PCVersionHistory.MajorVersionNumber = @SoftwareMajorVersionNumber
			AND PCVersionHistory.MinorVersionNumber = @SoftwareMinorVersionNumber)				
	BEGIN
	
		INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
		VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE())
		
	END

END
ELSE
BEGIN
	
	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient)
	VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MachineName, 1);
	
	SET @PcID = SCOPE_IDENTITY();

	IF NOT EXISTS (SELECT 1 FROM PCVersionHistory 
		WHERE PCVersionHistory.PcID = @PcID
		AND PCVersionHistory.MajorVersionNumber = @SoftwareMajorVersionNumber
		AND PCVersionHistory.MinorVersionNumber = @SoftwareMinorVersionNumber)	
					
	BEGIN
	
		INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
		VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE())
	
	END
END

GO


/****** Object:  StoredProcedure [dbo].[dp_addUserAddPCLinkPC]    Script Date: 05/13/2011 17:23:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addUserAddPCLinkPC]
(
	@EmailAddress nvarchar(100),
	@Password nvarchar(50),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Gender nvarchar(6),
	@DOB datetime,
	@TownCity nvarchar(100),
	@State nvarchar(100) = null,
	@Country nvarchar(100),
	@OccupationSector nvarchar(100),
	@EmploymentLevel nvarchar(100),
	@AnnualHouseholdIncome nvarchar(100),
	@UserGUID nvarchar(50),
	@MachineGUID nvarchar(50),
	@SoftwareMajorVersionNumber int,
	@SoftwareMinorVersionNumber int,
	@MACAddress nvarchar(20) = NULL,
	@MachineName nvarchar(100),
	@ChannelGUID nvarchar(50) OUTPUT
)

AS

DECLARE @UserID int;
DECLARE @ConsumerID int;
DECLARE @PublisherID int;
DECLARE @AccountTypeID int;
DECLARE @NormalAccountTypeID int;
DECLARE @TotalAvailableBytes bigint;
DECLARE @UserGUIDSuffix char(1);
DECLARE @CountryID int;
DECLARE @StateID int;
DECLARE @TownCityID int;
DECLARE @OccupationSectorID int;
DECLARE @EmploymentLevelID int;
DECLARE @AnnualHouseholdIncomeID int;
DECLARE @PcID int;

SET @UserGUIDSuffix = SUBSTRING(@UserGUID, LEN(@UserGUID), 1);

SELECT 
	@AccountTypeID = AccountTypes.ACCOUNTTYPE_ID,
	@NormalAccountTypeID = AccountTypes.ACCOUNTTYPE_ID,
	@TotalAvailableBytes = AccountTypes.TotalAvailableBytes
FROM 
	AccountTypes
WHERE
	AccountTypes.AccountType = 'Free';

SELECT 
	@CountryID = LocationTaxonomyTree.LOCATION_ID 
FROM 
	LocationTaxonomyTree 
WHERE 
	LocationName = @Country;

SELECT 
	@StateID = LocationTaxonomyTree.LOCATION_ID 
FROM 
	LocationTaxonomyTree 
WHERE 
	LocationName = @State AND ParentLocationID = @CountryID;

IF @StateID IS NOT NULL
	SELECT 
		@TownCityID = LocationTaxonomyTree.LOCATION_ID 
	FROM 
		LocationTaxonomyTree
	WHERE 
		LocationTree LIKE CONVERT(nvarchar(10), @CountryID) + '.' +  CONVERT(nvarchar(10), @StateID) + '%'
		AND LocationName = @TownCity;
ELSE
	SELECT 
		@TownCityID = LocationTaxonomyTree.LOCATION_ID 
	FROM 
		LocationTaxonomyTree
	WHERE 
		ParentLocationID = @CountryID
		AND LocationName = @TownCity;
		
SELECT
	@OccupationSectorID = SocioEconomicStatus.SOCIOECONOMICSTATUS_ID
FROM
	SocioEconomicStatus
WHERE
	SocioEconomicStatus.SocioEconomicType = 'O'
	AND SocioEconomicStatus.SocioEconomicValue = @OccupationSector;

SELECT
	@EmploymentLevelID = SocioEconomicStatus.SOCIOECONOMICSTATUS_ID
FROM
	SocioEconomicStatus
WHERE
	SocioEconomicStatus.SocioEconomicType = 'L'
	AND SocioEconomicStatus.SocioEconomicValue = @EmploymentLevel;
		
SELECT
	@AnnualHouseholdIncomeID = SocioEconomicStatus.SOCIOECONOMICSTATUS_ID
FROM
	SocioEconomicStatus
WHERE
	SocioEconomicStatus.SocioEconomicType = 'I'
	AND SocioEconomicStatus.SocioEconomicValue = @AnnualHouseholdIncome;
		
INSERT INTO Users(UserGUID, Username, UserPassword, AccountTypeID, AddDate)
VALUES (@UserGUID, @EmailAddress, @Password, @AccountTypeID, GETDATE()); 

SET @UserID = SCOPE_IDENTITY();

INSERT INTO Consumers(UserID, LocationTaxonomyTreeID, FirstName, LastName, EmailAddress, Gender, DOB, TownCity, State,
Country, OccupationSectorID, EmploymentLevelID, AnnualHouseholdIncomeID, AddDate)
VALUES
(@UserID, @TownCityID, @FirstName, @LastName, @EmailAddress, @Gender, @DOB, @TownCity, @State, @Country, @OccupationSectorID,
@EmploymentLevelID, @AnnualHouseholdIncomeID, GETDATE());

SET @ConsumerID = SCOPE_IDENTITY();

INSERT INTO PCs(PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, AddDate, ConsumerID, bLinkedToClient)
VALUES (@MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MachineName, GETDATE(), @ConsumerID, 1);

SET @PcID = SCOPE_IDENTITY();

INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE());

INSERT INTO Publishers(FirstName, LastName, EmailAddress, UserID, UsedBytes, TotalAvailableBytes)
VALUES (@FirstName, @LastName, @EmailAddress, @UserID, 0, @TotalAvailableBytes);

SET @PublisherID = SCOPE_IDENTITY();

INSERT INTO AssetContentFolders(PublisherID, AssetContentFolderName)
VALUES (@PublisherID, 'My Content Folder');

INSERT INTO SlideFolders(PublisherID, SlideFolderName)
VALUES (@PublisherID, 'My Slide Folder');

SET @ChannelGUID = CONVERT(nvarchar(38), NEWID()) + '_' + @UserGUIDSuffix;

INSERT INTO Channels(PublisherID, ChannelName, ChannelGUID, ImagePath, bHasDefaultThumbnail, bLocked, bAcceptPasswordRequests, NoContent, NoFollowers)
VALUES(@PublisherID, @FirstName + ' ' + @LastName, @ChannelGUID, @UserGUIDSuffix + '/' + @ChannelGUID + '.gif', 1, 0, 1, 0, 0);

GO


/****** Object:  StoredProcedure [dbo].[dp_removePCFromUninstall]    Script Date: 05/13/2011 17:24:54 ******/
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

	DELETE FROM PcChannels WHERE PcChannels.PcID = @PcID;

	DELETE FROM PCVersionHistory WHERE PCVersionHistory.PcID = @PcID;

	DELETE FROM PCs WHERE PCs.PC_ID = @PcID;

END

GO


/****** Object:  StoredProcedure [dbo].[dp_removeUserAccount]    Script Date: 05/13/2011 17:26:36 ******/
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
	
DELETE 
	PCs
FROM 
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;
	
DELETE FROM Consumers WHERE Consumers.UserID = @UserID;

DELETE FROM Publishers WHERE Publishers.UserID = @UserID;

DELETE FROM Users WHERE Users.USER_ID = @UserID;

GO

-- first row for existing PCs --
INSERT INTO PCVersionHistory (PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
SELECT PC_ID, MajorVersionNumber, MinorVersionNumber, AddDate
FROM PCs
WHERE PCs.PCGUID IS NOT NULL
AND MajorVersionNumber IS NOT NULL

GO