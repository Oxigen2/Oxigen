/****** Object:  StoredProcedure [dbo].[dp_addUserAddPCLinkPC]    Script Date: 05/04/2011 15:59:40 ******/
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

/****** Object:  StoredProcedure [dbo].[dp_addPCByGUID]    Script Date: 05/04/2011 16:02:28 ******/
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

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.Username = @EmailAddress;

INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient)
VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @PCName, 1);

GO

/****** Object:  StoredProcedure [dbo].[dp_getUserGUIDByUsername]    Script Date: 05/04/2011 17:57:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserGUIDByUsername]
(
@Username nvarchar(100),
@UserGUID nvarchar(50) OUTPUT
)

AS

SELECT
	@UserGUID = Users.UserGUID
FROM
	Users
WHERE
	Users.Username = @Username;
	
IF @UserGUID IS NULL
	SET @UserGUID = '-'

GO

/****** Object:  StoredProcedure [dbo].[dp_addPCByUserGUIDMachineGUID]    Script Date: 05/04/2011 18:01:08 ******/
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

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.UserGUID = @UserGUID;
	
IF EXISTS (SELECT 1 FROM PCs WHERE PCs.PCGUID = @MachineGUID)
BEGIN
	
	UPDATE 
		PCs
	SET
		MajorVersionNumber = @SoftwareMajorVersionNumber,
		MinorVersionNumber = @SoftwareMinorVersionNumber,
		PcName = @MachineName,
		bLinkedToClient = 1
	FROM
		PCs
	WHERE 
		PCs.PCGUID = @MachineGUID

END
ELSE
BEGIN
	
	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, MajorVersionNumber, MinorVersionNumber, PcName, bLinkedToClient)
	VALUES (@ConsumerID, @MachineGUID, @MACAddress, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MachineName, 1);

END

GO

/****** Object:  StoredProcedure [dbo].[dp_addStreamByPCGUID]    Script Date: 05/05/2011 10:20:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addStreamByPCGUID]
(
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS

DECLARE @PCID int;
DECLARE @UserGUID nvarchar(50);
DECLARE @ChannelID int;

SELECT
	@UserGUID = Users.UserGUID,
	@PCID = PCs.PC_ID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN PCs ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	PCs.PCGUID = @MachineGUID

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;
	
IF NOT EXISTS (SELECT 1 FROM PcChannels WHERE PcChannels.PcID = @PCID AND PcChannels.ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels(ChannelID, ChannelWeighting, PCGUID, PcID, UserGUID)
	VALUES (@ChannelID, @ChannelWeighting, @MachineGUID, @PCID, @UserGUID);

END

GO

/****** Object:  StoredProcedure [dbo].[dp_removeAllStreamsByMachineGUID]    Script Date: 05/05/2011 11:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeAllStreamsByMachineGUID]
(
@MachineGUID nvarchar(50)
)

AS

DECLARE @PCID int;

SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;

DELETE FROM
	PcChannels
WHERE
	PcID = @PCID;

GO

/****** Object:  StoredProcedure [dbo].[dp_addStreamByMachineGUID]    Script Date: 05/05/2011 11:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addStreamByMachineGUID]
(
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS

DECLARE @PCID int;
DECLARE @UserGUID nvarchar(50);
DECLARE @ChannelID int;

SELECT
	@UserGUID = Users.UserGUID,
	@PCID = PCs.PC_ID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN PCs ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	PCs.PCGUID = @MachineGUID;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;
	
IF NOT EXISTS (SELECT 1 FROM PcChannels WHERE PcChannels.PcID = @PCID AND PcChannels.ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels(ChannelID, ChannelWeighting, PCGUID, PcID, UserGUID)
	VALUES (@ChannelID, @ChannelWeighting, @MachineGUID, @PCID, @UserGUID);

END

GO

/****** Object:  StoredProcedure [dbo].[dp_removeStreamByMachineGUID]    Script Date: 05/05/2011 11:42:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeStreamByMachineGUID]
(
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS

DECLARE @PCID int;
DECLARE @ChannelID int;
	
SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;
	

DELETE FROM
	PcChannels
WHERE
	PcChannels.PcID = @PCID
	AND PcChannels.ChannelID = @ChannelID;

GO