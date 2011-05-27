ALTER TABLE PCs DROP COLUMN MajorVersionNumber, MinorVersionNumber;
GO

/****** Object:  StoredProcedure [dbo].[dp_addPCByGUID]    Script Date: 05/27/2011 16:54:19 ******/
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

INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, PcName, bLinkedToClient, bDeleted)
VALUES (@ConsumerID, @MachineGUID, @MACAddress, @PCName, 1, 0);

SET @PcID = SCOPE_IDENTITY();

INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE())

GO

/****** Object:  StoredProcedure [dbo].[dp_addPCByUserGUIDMachineGUID]    Script Date: 05/27/2011 16:55:30 ******/
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
	
	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, PcName, bLinkedToClient)
	VALUES (@ConsumerID, @MachineGUID, @MACAddress, @MachineName, 1);
	
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

/****** Object:  StoredProcedure [dbo].[dp_addUserAddPCLinkPC]    Script Date: 05/27/2011 16:57:13 ******/
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
		
INSERT INTO Users(UserGUID, Username, UserPassword, AccountTypeID, bDeleted, AddDate)
VALUES (@UserGUID, @EmailAddress, @Password, @AccountTypeID, 0, GETDATE()); 

SET @UserID = SCOPE_IDENTITY();

INSERT INTO Consumers(UserID, LocationTaxonomyTreeID, FirstName, LastName, EmailAddress, Gender, DOB, TownCity, State,
Country, OccupationSectorID, EmploymentLevelID, AnnualHouseholdIncomeID, AddDate)
VALUES
(@UserID, @TownCityID, @FirstName, @LastName, @EmailAddress, @Gender, @DOB, @TownCity, @State, @Country, @OccupationSectorID,
@EmploymentLevelID, @AnnualHouseholdIncomeID, GETDATE());

SET @ConsumerID = SCOPE_IDENTITY();

INSERT INTO PCs(PCGUID, MACAddress, PcName, AddDate, ConsumerID, bLinkedToClient, bDeleted)
VALUES (@MachineGUID, @MACAddress, @MachineName, GETDATE(), @ConsumerID, 1, 0);

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

/****** Object:  StoredProcedure [dbo].[dp_createPcIfNotExists]    Script Date: 05/27/2011 16:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[dp_createPcIfNotExists]

(@UserGUID nvarchar(50), 
@MachineName nvarchar(50),
@MacAddress nvarchar(20), 
@MajorVersionNumber int,
@MinorVersionNumber int,
@MachineGUID nvarchar(50) OUTPUT)

AS

DECLARE @PcID int;
DECLARE @ConsumerID int;
DECLARE @RandomCharacter char(1);
DECLARE @ValidCharacters char(26);
DECLARE @RandomNumber float;
DECLARE @RandomNumberInt tinyint;
	
IF NOT EXISTS(SELECT 1 FROM	PCs WHERE PCs.MACAddress = @MacAddress)
BEGIN

	SET @ValidCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
	
	SET @RandomNumber  = RAND();
	SET @RandomNumberInt = Convert(tinyint, (25 * @RandomNumber + 1));	
	SET @RandomCharacter = SUBSTRING(@ValidCharacters, @RandomNumberInt, 1);

	SELECT
		@ConsumerID = Consumers.CONSUMER_ID
	FROM
		Consumers INNER JOIN Users on Consumers.UserID = Users.USER_ID
	WHERE
		Users.UserGUID = @UserGUID;
	
	-- create a new pc profile --
	SET @MachineGUID = NEWID();
	SET @MachineGUID = @MachineGUID + '_' + @RandomCharacter;

	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, PcName, bLinkedToClient, bDeleted, AddDate)
	VALUES (@ConsumerID, @MachineGUID, @MacAddress, @MachineName, 1, 0, getdate());

	SET @PcID = SCOPE_IDENTITY();

	INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
	VALUES (@PcID, @MajorVersionNumber, @MinorVersionNumber, GETDATE())
END
ELSE
	SELECT 
		@MachineGUID = PCs.PCGUID
	FROM
		PCs
	WHERE
		PCs.MACAddress = @MacAddress;

GO

/****** Object:  StoredProcedure [dbo].[dp_editCompareMacAddressesLink]    Script Date: 05/27/2011 17:12:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_editCompareMacAddressesLink]
(
@MacAddressClient nvarchar(20),
@UserGUID nvarchar(50),
@NewMachineGUID nvarchar(50),
@SoftwareMajorVersionNumber int,
@SoftwareMinorVersionNumber int,
@bMatch bit OUTPUT
)

AS

IF EXISTS (SELECT 1 FROM PCs WHERE MACAddress = @MacAddressClient)
BEGIN

	SET @bMatch = 1;
	
	UPDATE
		PCs
	SET
		PCGUID = @NewMachineGUID,
		bLinkedToClient = 1
	WHERE
		MACAddress = @MacAddressClient;
		
	UPDATE 
		PcChannels
	SET
		PCGUID = @NewMachineGUID
	FROM 
		PcChannels INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
	WHERE
		PCs.MACAddress = @MacAddressClient;

END
ELSE
BEGIN

	DECLARE @ConsumerID int;
	
	SET @bMatch = 0;
	
	SELECT
		@ConsumerID = Consumers.CONSUMER_ID
	FROM
		Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
		WHERE Users.UserGUID = @UserGUID;
			
	INSERT INTO PCs(PCGUID, MACAddress, bLinkedToClient, ConsumerID, AddDate, PcName)
	VALUES (@NewMachineGUID, @MacAddressClient, 1, @ConsumerID, GETDATE(), 'New PC');
	
END

GO

/****** Object:  StoredProcedure [dbo].[dp_removePCSubscriptionsLinkDirty]    Script Date: 05/27/2011 17:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_removePCSubscriptionsLinkDirty]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@MACAddress nvarchar(20),
@SoftwareMajorVersionNumber int,
@SoftwareMinorVersionNumber int,
@PCID int
)

AS

UPDATE
	Channels
SET
	NoFollowers = NoFollowers - 1
FROM
	PcChannels INNER JOIN Channels ON PcChannels.ChannelID = Channels.CHANNEL_ID
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PcID = @PCID;

DELETE 
	PCChannels
FROM
	PCChannels
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PcID = @PCID;
	
UPDATE
	PCs
SET
	PCs.PCGUID = @MachineGUID,
	PCs.MACAddress = @MACAddress,
	PCs.bLinkedToClient = 1,
	PCs.MadeLastDirtyDate = GETDATE()
WHERE
	PCs.PC_ID = @PCID;

GO