ALTER TABLE PCs ADD bDeleted BIT;
GO
UPDATE PCs SET bDeleted = 0;

GO
ALTER TABLE Users ADD bDeleted BIT;
GO
UPDATE Users SET bDeleted = 0;
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

INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient, bDeleted)
VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @PCName, 1, 0);

SET @PcID = SCOPE_IDENTITY();

INSERT INTO PCVersionHistory(PcID, MajorVersionNumber, MinorVersionNumber, UpdatedAt)
VALUES (@PcID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, GETDATE())

GO


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
	
	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient, bDeleted)
	VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MachineName, 1, 0);
	
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
		
INSERT INTO Users(UserGUID, Username, UserPassword, AccountTypeID, bDeleted, AddDate)
VALUES (@UserGUID, @EmailAddress, @Password, @AccountTypeID, 0, GETDATE()); 

SET @UserID = SCOPE_IDENTITY();

INSERT INTO Consumers(UserID, LocationTaxonomyTreeID, FirstName, LastName, EmailAddress, Gender, DOB, TownCity, State,
Country, OccupationSectorID, EmploymentLevelID, AnnualHouseholdIncomeID, AddDate)
VALUES
(@UserID, @TownCityID, @FirstName, @LastName, @EmailAddress, @Gender, @DOB, @TownCity, @State, @Country, @OccupationSectorID,
@EmploymentLevelID, @AnnualHouseholdIncomeID, GETDATE());

SET @ConsumerID = SCOPE_IDENTITY();

INSERT INTO PCs(PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, AddDate, ConsumerID, bLinkedToClient, bDeleted)
VALUES (@MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MachineName, GETDATE(), @ConsumerID, 1, 0);

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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addTempPcProfileRegistered]

(@UserID int)

AS

DECLARE @ConsumerID int;

SELECT @ConsumerID = Consumers.CONSUMER_ID FROM Consumers WHERE Consumers.UserID = @UserID;

IF NOT EXISTS (SELECT 1 FROM PCs 
				INNER JOIN Consumers on PCs.ConsumerID = Consumers.CONSUMER_ID 
				WHERE Consumers.UserID = @UserID)
				
	
	INSERT INTO PCs(PcName, ConsumerID, bLinkedToClient, bDeleted) VALUES ('Temporary PC Profile 1', @ConsumerID, 0, 0);


GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addTempPcProfileUnregistered]

(@PCProfileToken nvarchar(50))

AS

IF NOT EXISTS (SELECT 1 FROM PCs WHERE PCGUID = @PCProfileToken)
	INSERT INTO PCs(PcName, PCGUID, bLinkedToClient, bDeleted) VALUES ('Temporary PC Profile 1', @PCProfileToken, 0, 0);

GO


/****** Object:  StoredProcedure [dbo].[dp_createPcIfNotExists]    Script Date: 05/20/2011 17:39:16 ******/
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

	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, PcName, MajorVersionNumber, MinorVersionNumber, bLinkedToClient, bDeleted, AddDate)
	VALUES (@ConsumerID, @MachineGUID, @MacAddress, @MachineName, @MajorVersionNumber, @MinorVersionNumber, 1, 0, getdate());

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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_getPCList]
(
@UserID int
)

AS

SELECT
	PC_ID,
	PcName,
	bLinkedToClient
FROM
	PCs,
	Consumers
WHERE
	PCs.ConsumerID = Consumers.CONSUMER_ID
	AND PCs.bDeleted = 0
	AND Consumers.UserID = @UserID;

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
		PCs.bDeleted = 1
	WHERE 
		PCs.PC_ID = @PcID;

END

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addPublisherConsumer]
(
@FirstName nvarchar(50),
@LastName nvarchar(50),
@EmailAddress nvarchar(100),
@Password nvarchar(10),
@ChannelGUID NVARCHAR(40) OUTPUT,
@UserID int OUTPUT,
@TotalAvailableBytes bigint OUTPUT
)

AS

-- BEGIN TRANSACTION --

DECLARE @PublisherID int;
DECLARE @UserGUID nvarchar(50);
DECLARE @char CHAR(1);
DECLARE @UsersNameFirstLetter CHAR(1);
DECLARE @NormalAccountTypeID int;

SET @UserGUID = CONVERT(nvarchar(38), NEWID()) + '_' + SUBSTRING(@LastName, 1, 1);
SET @UsersNameFirstLetter = SUBSTRING(@FirstName, 1, 1);

SELECT
	@NormalAccountTypeID = AccountTypes.ACCOUNTTYPE_ID,
	@TotalAvailableBytes = AccountTypes.TotalAvailableBytes
FROM
	AccountTypes
WHERE
	AccountTypes.AccountType = 'Free';

-- add user, affect the LastLoginDate column as user will also be logged in after control returns to presentation layer --
INSERT INTO Users (UserGUID, Username, UserPassword, LastLoginDate, AccountTypeID, bDeleted)
VALUES (@UserGUID, @EmailAddress, @Password, GETDATE(), @NormalAccountTypeID, 0); 

SET @UserID = SCOPE_IDENTITY();

INSERT INTO Consumers (FirstName, LastName, EmailAddress, UserID)
VALUES (@FirstName, @LastName, @EmailAddress, @UserID);

INSERT INTO Publishers(FirstName, LastName, EmailAddress, UserID, UsedBytes, TotalAvailableBytes)
VALUES (@FirstName, @LastName, @EmailAddress, @UserID, 0, @TotalAvailableBytes);

SET @PublisherID = SCOPE_IDENTITY();

-- Create default folders and channel for publisher --
INSERT INTO AssetContentFolders(PublisherID, AssetContentFolderName)
VALUES (@PublisherID, 'Content Folder 1');

INSERT INTO SlideFolders(PublisherID, SlideFolderName)
VALUES (@PublisherID, 'Slide Folder 1');

SET @ChannelGUID = CONVERT(nvarchar(38), NEWID()) + '_' + @UsersNameFirstLetter

INSERT INTO Channels(PublisherID, ChannelName, ChannelGUID, ImagePath, bHasDefaultThumbnail, bLocked, bAcceptPasswordRequests, NoContent, NoFollowers)
VALUES(@PublisherID, @FirstName + ' ' + @LastName, @ChannelGUID, @UsersNameFirstLetter + '/' + @ChannelGUID + '.gif', 1, 0, 1, 0, 0);

-- COMMIT TRANSACTION --
RETURN @UserID;

GO


ALTER TABLE PCs ALTER Column bDeleted BIT NOT NULL;

GO

ALTER TABLE Users ALTER Column bDeleted BIT NOT NULL;

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
	Users.bDeleted = 1
WHERE
	Users.USER_ID = @UserID;

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_getUserLogin]
(
@Username nvarchar(50),
@Password nvarchar(45)
)

AS

DECLARE @UserID int;

SELECT
	@UserID = USER_ID
FROM
	Users 
WHERE Username = @Username 
	  AND UserPassword = @Password;

IF (@@ROWCOUNT <> 1)
	RETURN;
ELSE
BEGIN

	UPDATE
		Users
	SET
		LastLoginDate = getDate()
	WHERE
		Users.USER_ID = @UserID;

	SELECT
		Users.USER_ID,
		Publishers.FirstName,
		Publishers.LastName,
		Publishers.EmailAddress,
		Publishers.UsedBytes,
		Publishers.TotalAvailableBytes
	FROM
		Users,
		Publishers
	WHERE
		Users.USER_ID = Publishers.UserID
		AND Users.USER_ID = @UserID
		AND Users.bDeleted = 0;

END

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_getUserHasRealPCs]

(@UserID int)

AS

SELECT 1 FROM PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE Consumers.UserID = @UserID
AND PCs.bLinkedToClient = 1
AND PCs.bDeleted = 0;

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addTempPcProfileRegistered]

(@UserID int)

AS

DECLARE @ConsumerID int;

SELECT @ConsumerID = Consumers.CONSUMER_ID FROM Consumers WHERE Consumers.UserID = @UserID;

IF NOT EXISTS (SELECT 1 FROM PCs 
				INNER JOIN Consumers on PCs.ConsumerID = Consumers.CONSUMER_ID 
				WHERE Consumers.UserID = @UserID
				AND PCs.bDeleted = 0)
				
	
	INSERT INTO PCs(PcName, ConsumerID, bLinkedToClient, bDeleted) VALUES ('Temporary PC Profile 1', @ConsumerID, 0, 0);

GO
