
/****** Object:  User [SQL2008Access]    Script Date: 04/04/2011 13:38:31 ******/
CREATE USER [SQL2008Access] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  FullTextCatalog [Channel Data]    Script Date: 04/04/2011 13:38:31 ******/
CREATE FULLTEXT CATALOG [Channel Data]WITH ACCENT_SENSITIVITY = ON
AUTHORIZATION [dbo]
GO
/****** Object:  Table [dbo].[SocioEconomicStatus]    Script Date: 04/04/2011 13:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SocioEconomicStatus](
	[SOCIOECONOMICSTATUS_ID] [int] IDENTITY(1,1) NOT NULL,
	[SocioEconomicCode]  AS (substring([SocioEconomicType],(1),(1))+CONVERT([nvarchar](9),[SOCIOECONOMICSTATUS_ID],(0))),
	[SocioEconomicType] [char](1) NOT NULL,
	[SocioEconomicValue] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SocioEconomicStatus] PRIMARY KEY CLUSTERED 
(
	[SOCIOECONOMICSTATUS_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocationTaxonomyTree]    Script Date: 04/04/2011 13:38:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationTaxonomyTree](
	[LOCATION_ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentLocationID] [int] NULL,
	[LocationTree] [nvarchar](4000) NULL,
	[LocationName] [nvarchar](50) NOT NULL,
	[bHasChildren] [bit] NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_LocationTaxonomyTree] PRIMARY KEY CLUSTERED 
(
	[LOCATION_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_SplitCDLToTableInt]    Script Date: 04/04/2011 13:38:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  FUNCTION [dbo].[fn_SplitCDLToTableInt]
(
	@RowData nvarchar(2000),
	@SplitOn nvarchar(5)
)  
RETURNS @RtnValue TABLE 
(
	IDENTITY_ID int IDENTITY(1,1),
	Data int
) 
AS  
BEGIN 
	DECLARE @Cnt int
	SET @Cnt = 1

	WHILE (CHARINDEX(@SplitOn, @RowData) > 0)
	BEGIN
		INSERT INTO @RtnValue (data)
		SELECT 
			Data = LTRIM(RTRIM(SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn,@RowData) - 1)))

		SET @RowData = SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData) + 1, LEN(@RowData))
		SET @Cnt = @Cnt + 1
	END
	
	INSERT INTO @RtnValue (data)
	SELECT Data = LTRIM(RTRIM(@RowData))

	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_getLetterFromNumber]    Script Date: 04/04/2011 13:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_getLetterFromNumber]
	(
	@OrderNumber int
	)
RETURNS CHAR(1)
AS
	BEGIN
	
	RETURN
	
	CASE @OrderNumber
		WHEN 1 THEN 'A'
		WHEN 2 THEN 'B'
		WHEN 3 THEN 'C'
		WHEN 4 THEN 'D'
		WHEN 5 THEN 'E'
		WHEN 6 THEN 'F'
		WHEN 7 THEN 'G'
		WHEN 8 THEN 'H'
		WHEN 9 THEN 'I'
		WHEN 10 THEN 'J'
		WHEN 11 THEN 'K'
		WHEN 12 THEN 'L'
		WHEN 13 THEN 'M'
		WHEN 14 THEN 'N'
		WHEN 15 THEN 'O'
		WHEN 16 THEN 'P'
		WHEN 17 THEN 'Q'
		WHEN 18 THEN 'R'
		WHEN 19 THEN 'S'
		WHEN 20 THEN 'T'
		WHEN 21 THEN 'U'
		WHEN 22 THEN 'V'
		WHEN 23 THEN 'W'
		WHEN 24 THEN 'X'
		WHEN 25 THEN 'Y'
 	  WHEN 26 THEN 'Z'
 	  ELSE 'A' 	 
 	 END
 	 
	END
GO
/****** Object:  Table [dbo].[ChannelPasswordUnlock]    Script Date: 04/04/2011 13:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChannelPasswordUnlock](
	[CHANNELPASSWORDUNLOCK_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ChannelID] [int] NOT NULL,
 CONSTRAINT [PK_ChannelPasswordUnlock_Table] PRIMARY KEY CLUSTERED 
(
	[CHANNELPASSWORDUNLOCK_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 04/04/2011 13:38:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CATEGORY_ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentCategoryID] [int] NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[bHasChildren] [bit] NOT NULL,
	[CTT] [nvarchar](4000) NULL,
	[CTTLevel1] [int] NULL,
	[CTTLevel2] [int] NULL,
	[CTTLevel3] [int] NULL,
	[CTTLevel4] [int] NULL,
	[CTTLevel5] [int] NULL,
	[CTTLevel6] [int] NULL,
	[CTTLevel7] [int] NULL,
	[CTTLevel8] [int] NULL,
	[CTTLevel9] [int] NULL,
	[CTTLevel10] [int] NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Channel_Table] PRIMARY KEY CLUSTERED 
(
	[CATEGORY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelsByLetter]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelsByLetter]
(
@ChannelLetter char(1),
@SortBy nvarchar(12)
)

AS

DECLARE @ShellSQL nvarchar(300);
DECLARE @OrderSQL nvarchar(18);

IF (@SortBy = 'Alphabetical')
	SET @OrderSQL = 'ChannelName';

IF (@SortBy = 'Popularity')
	SET @OrderSQL = 'NoFollowers DESC';

SET @ShellSQL = 'SELECT ' +
					'Channel_ID, ' +
					'ChannelName ' +
				'FROM ' +
					'Channels ' +
				'WHERE  ' +
					'Channels.ChannelName LIKE ''' + @ChannelLetter + '%'' ' +
					'AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID) ' +
				'ORDER BY ' + @OrderSQL;

					
EXEC (@ShellSQL);
GO
/****** Object:  Table [dbo].[AdvertTaxonomyTrees]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertTaxonomyTrees](
	[ADVERTTAXONOMYTREE_ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentAdvertTaxonomyTreeID] [int] NULL,
	[AdvertTaxonomyName] [nvarchar](50) NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_AdvertTaxonomyTrees] PRIMARY KEY CLUSTERED 
(
	[ADVERTTAXONOMYTREE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTypes]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTypes](
	[ACCOUNTTYPE_ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [nvarchar](20) NOT NULL,
	[AccountLevel] [int] NOT NULL,
	[TotalAvailableBytes] [bigint] NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[ACCOUNTTYPE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_addGeoElement]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addGeoElement]
(
@ElementName nvarchar(100),
@ParentElementID int = NULL
)

AS

INSERT INTO LocationTaxonomyTree(LocationName, ParentLocationID, bhaschildren) VALUES (@ElementName, @ParentElementID, 0);

RETURN SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[dp_editGeoHasChildren]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editGeoHasChildren]

AS

update LocationTaxonomyTree
set bhaschildren = 1
WHERE
LOCATION_ID IN (SELECT LT.ParentLocationID FROM LocationTaxonomyTree LT);
GO
/****** Object:  StoredProcedure [dbo].[dp_getCountries]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getCountries]

AS

SELECT
	LocationTaxonomyTree.LOCATION_ID,
	LocationTaxonomyTree.LocationName
FROM
	LocationTaxonomyTree
WHERE
	LocationTaxonomyTree.ParentLocationID IS NULL; -- Only countries (top level CTT nodes) have a null ParentLocationID
GO
/****** Object:  StoredProcedure [dbo].[dp_getChildGeoTTNodes]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChildGeoTTNodes]

(@ParentLocationID int)

AS

SELECT
	LocationTaxonomyTree.LOCATION_ID,
	LocationTaxonomyTree.LocationName,
	LocationTaxonomyTree.bHasChildren
FROM
	LocationTaxonomyTree
WHERE
	LocationTaxonomyTree.ParentLocationID = @ParentLocationID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getCategoryName]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getCategoryName]

(
	@CategoryID int	
)

AS

SELECT
	CategoryName
FROM
	Categories
WHERE
	CATEGORY_ID = @CategoryID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getCategoryChildList]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getCategoryChildList]

(
	@ParentCategoryID int	
)

AS

IF (@ParentCategoryID = -1)

SELECT
	CATEGORY_ID,
	CategoryName,
	bHasChildren
FROM
	Categories
WHERE
	ParentCategoryID IS NULL
ORDER BY CategoryName;

ELSE
BEGIN

SELECT
	CATEGORY_ID,
	CategoryName,
	bHasChildren
FROM
	Categories
WHERE
	ParentCategoryID = @ParentCategoryID
ORDER BY CategoryName;

END
GO
/****** Object:  StoredProcedure [dbo].[dp_getCategory]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getCategory]

(
	@CategoryID int	
)

AS

SELECT
	CATEGORY_ID,
	CategoryName,
	bHasChildren
FROM
	Categories
WHERE
	CATEGORY_ID = @CategoryID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getStates]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getStates]

AS

SELECT
	LtStates.LOCATION_ID,
	LtStates.LocationName
FROM
	LocationTaxonomyTree LtStates INNER JOIN LocationTaxonomyTree LtCountries
		ON	LtStates.ParentLocationID = LtCountries.LOCATION_ID
WHERE
	LtCountries.LocationName = 'United States';
GO
/****** Object:  StoredProcedure [dbo].[dp_getSocioEconomicStatuses]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSocioEconomicStatuses]

(@Type CHAR(1))

AS

SELECT
	SocioEconomicStatus.SOCIOECONOMICSTATUS_ID,
	SocioEconomicStatus.SocioEconomicValue
FROM
	SocioEconomicStatus
WHERE
	SocioEconomicStatus.SocioEconomicType = @Type;
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeID] [int] NOT NULL,
	[LocationID] [int] NULL,
	[UserGUID] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](45) NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [_dta_index_Users_5_2108586600__K2_11_12] ON [dbo].[Users] 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [_dta_index_Users_5_888390234__K4_K1] ON [dbo].[Users] 
(
	[UserGUID] ASC,
	[USER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publishers]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publishers](
	[PUBLISHER_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DisplayName]  AS (([FirstName]+' ')+[LastName]),
	[EmailAddress] [nvarchar](100) NOT NULL,
	[UsedBytes] [bigint] NOT NULL,
	[TotalAvailableBytes] [bigint] NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED 
(
	[PUBLISHER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNQ_Publishers_EmailAddress] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [_dta_index_Publishers_5_2108586600__K2_11_12] ON [dbo].[Publishers] 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserExistsByUserCredentials]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserExistsByUserCredentials]
(
@EmailAddress nvarchar(50),
@Password nvarchar(50),
@UserGUID nvarchar(50) OUTPUT
)

AS

SELECT
	@UserGUID = Users.UserGUID
FROM 
	Users 
WHERE Users.Username = @EmailAddress 
AND Users.UserPassword = @Password;

IF (@@ROWCOUNT <> 0)
	RETURN 1;

IF EXISTS (SELECT 1 
			FROM 
				Users 
			WHERE Users.Username = @EmailAddress)
RETURN -1; -- email exists, password wrong --

RETURN -2; -- neither email nor password exists --
GO
/****** Object:  StoredProcedure [dbo].[dp_editUserPassword]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editUserPassword]

(@Email nvarchar(100),
@NewPassword nvarchar(45))

AS

UPDATE
	Users
SET
	UserPassword = @NewPassword
WHERE
	Username = @Email;
GO
/****** Object:  Table [dbo].[Advertisers]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advertisers](
	[ADVERTISER_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DisplayName]  AS (([FirstName]+' ')+[LastName]),
	[EmailAddress] [nvarchar](100) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Advertisers] PRIMARY KEY CLUSTERED 
(
	[ADVERTISER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consumers]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consumers](
	[CONSUMER_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[LocationTaxonomyTreeID] [int] NULL,
	[OccupationSectorID] [int] NULL,
	[EmploymentLevelID] [int] NULL,
	[AnnualHouseholdIncomeID] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DisplayName]  AS (([FirstName]+' ')+[LastName]),
	[EmailAddress] [nvarchar](100) NOT NULL,
	[Gender] [nvarchar](6) NULL,
	[DOB] [datetime] NULL,
	[TownCity] [nvarchar](100) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](100) NULL,
	[OccupationSector] [nvarchar](100) NULL,
	[EmploymentLevel] [nvarchar](100) NULL,
	[AnnualHouseholdIncome] [nvarchar](100) NULL,
	[AddDate] [date] NOT NULL,
	[EditDate] [date] NULL,
 CONSTRAINT [PK_Consumers] PRIMARY KEY CLUSTERED 
(
	[CONSUMER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UNQ_Consumers_EmailAddress] UNIQUE NONCLUSTERED 
(
	[EmailAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [_dta_index_Consumers_5_2108586600__K2_11_12] ON [dbo].[Consumers] 
(
	[UserID] ASC
)
INCLUDE ( [Gender],
[DOB]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Channels]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channels](
	[CHANNEL_ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NULL,
	[PublisherID] [int] NOT NULL,
	[ChannelName] [nvarchar](50) NOT NULL,
	[ChannelGUID] [nvarchar](40) NULL,
	[ChannelDescription] [nvarchar](200) NULL,
	[ChannelLongDescription] [nvarchar](4000) NULL,
	[Keywords] [nvarchar](4000) NULL,
	[ImagePath] [nvarchar](100) NULL,
	[bHasDefaultThumbnail] [bit] NOT NULL,
	[bLocked] [bit] NOT NULL,
	[bAcceptPasswordRequests] [bit] NOT NULL,
	[ChannelPassword] [nvarchar](10) NULL,
	[ChannelGUIDSuffix]  AS (substring([ChannelGUID],len([ChannelGUID]),(1))),
	[NoContent] [int] NOT NULL,
	[NoFollowers] [int] NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
	[MadeDirtyLastDate] [datetime] NULL,
	[ContentLastAddedDate] [datetime] NULL,
 CONSTRAINT [PK_Channels] PRIMARY KEY CLUSTERED 
(
	[CHANNEL_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetContentFolders]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetContentFolders](
	[ASSETCONTENTFOLDER_ID] [int] IDENTITY(1,1) NOT NULL,
	[PublisherID] [int] NOT NULL,
	[AssetContentFolderName] [nvarchar](50) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_AssetContentFolders] PRIMARY KEY CLUSTERED 
(
	[ASSETCONTENTFOLDER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adverts]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adverts](
	[ADVERT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AdvertiserID] [int] NOT NULL,
	[AdvertName] [nvarchar](100) NOT NULL,
	[Schedule] [nvarchar](4000) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_Adverts] PRIMARY KEY CLUSTERED 
(
	[ADVERT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_editUserDetailsFromWebsite]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editUserDetailsFromWebsite]
(
@UserID int,
@EmailAddress nvarchar(100),
@Password nvarchar(10),
@FirstName nvarchar(50),
@LastName nvarchar(50),
@Gender nvarchar(6),
@DOB datetime,
@TownCityID int,
@OccupationSectorID int,
@EmploymentLevelID int,
@AnnualHouseholdIncomeID int,
@bUpdatePassword bit,
@bEmailAddressExists bit OUTPUT
)

AS


IF EXISTS (SELECT 1 FROM Users WHERE USER_ID != @UserID AND Username = @EmailAddress)
BEGIN

	SET @bEmailAddressExists = 1;

	RETURN;	
END
ELSE
	SET @bEmailAddressExists = 0;

UPDATE Consumers
	SET 
	EmailAddress = @EmailAddress,
	FirstName = @FirstName,
	LastName = @LastName,
	Gender = @Gender,
	DOB = @DOB,
	LocationTaxonomyTreeID = @TownCityID,
	OccupationSectorID = @OccupationSectorID,
	EmploymentLevelID = @EmploymentLevelID,
	AnnualHouseholdIncomeID = @AnnualHouseholdIncomeID
FROM
	Consumers 
WHERE
	Consumers.UserID = @UserID;
	

UPDATE 
	Publishers
SET 
	EmailAddress = @EmailAddress,
	FirstName = @FirstName,
	LastName = @LastName
FROM
	Publishers
WHERE
	Publishers.UserID = @UserID;
	
UPDATE 
	Users
SET 
	Username = @EmailAddress
WHERE
	Users.USER_ID = @UserID;
	
IF (@bUpdatePassword = 1)
BEGIN

	UPDATE
		Users
	SET
		UserPassword = @Password
	WHERE
		Users.USER_ID = @UserID;

END
GO
/****** Object:  StoredProcedure [dbo].[dp_editUserDetails]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editUserDetails]
(
@EmailAddress nvarchar(100),
@Password nvarchar(10),
@FirstName nvarchar(50),
@LastName nvarchar(50),
@Gender nvarchar(6),
@DOB datetime,
@TownCity nvarchar(100),
@State nvarchar(100) = null,
@Country nvarchar(100),
@OccupationSector nvarchar(100),
@EmploymentLevel nvarchar(100),
@AnnualHouseholdIncome nvarchar(100)
)

AS

DECLARE @CountryID int;
DECLARE @StateID int;
DECLARE @TownCityID int;
DECLARE @OccupationSectorID int;
DECLARE @EmploymentLevelID int;
DECLARE @AnnualHouseholdIncomeID int;

SELECT 
	@CountryID = LocationTaxonomyTree.LOCATION_ID 
FROM 
	LocationTaxonomyTree 
WHERE 
	LocationName = @Country
	AND ParentLocationID IS NULL;

SELECT 
	@StateID = LocationTaxonomyTree.LOCATION_ID 
FROM 
	LocationTaxonomyTree 
WHERE 
	LocationName = @State AND ParentLocationID = @CountryID
	AND ParentLocationID IS NOT NULL
	AND bHasChildren = 1;

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

UPDATE Consumers
	SET 
	FirstName = @FirstName,
	LastName = @LastName,
	Gender = @Gender,
	DOB = @DOB,
	LocationTaxonomyTreeID = @TownCityID,
	Country= @Country,
	State = @State,
	TownCity = @TownCity,
	OccupationSectorID = @OccupationSectorID,
	EmploymentLevelID = @EmploymentLevelID,
	AnnualHouseholdIncomeID = @AnnualHouseholdIncomeID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.Username = @EmailAddress
	AND Users.UserPassword = @Password;
GO
/****** Object:  StoredProcedure [dbo].[dp_getEmailAddressExists]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getEmailAddressExists]

(
@EmailAddress nvarchar(100)
)

AS

SELECT 1 FROM Consumers WHERE EmailAddress = @EmailAddress;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPublisherConsumerEmailAddressExist]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPublisherConsumerEmailAddressExist]
(
@EmailAddress nvarchar(100),
@EmailAddressExist bit OUTPUT
)

AS

IF EXISTS (SELECT 1 FROM Consumers WHERE EmailAddress = @EmailAddress)
	SET @EmailAddressExist = 1;
ELSE
	SET @EmailAddressExist = 0;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserDetailsFromWebsite]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserDetailsFromWebsite]

(@UserID int)

AS

SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	LtCountry.LOCATION_ID AS CountryID,
	NULL AS StateID,
	LtTownCity.LOCATION_ID AS TownCityID,
	Consumers.OccupationSectorID,
	Consumers.EmploymentLevelID,
	Consumers.AnnualHouseholdIncomeID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN LocationTaxonomyTree LtTownCity ON LtTownCity.LOCATION_ID = Consumers.LocationTaxonomyTreeID
	INNER JOIN LocationTaxonomyTree LtCountry ON LtCountry.LOCATION_ID = LtTownCity.ParentLocationID
WHERE
	LtCountry.ParentLocationID IS NULL -- Only two levels up the tree, the top of which is the country.
	AND Users.USER_ID = @UserID
UNION
SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	LtCountry.LOCATION_ID AS CountryID,
	LtState.LOCATION_ID AS StateID,
	LtTownCity.LOCATION_ID AS TownCityID,
	Consumers.OccupationSectorID,
	Consumers.EmploymentLevelID,
	Consumers.AnnualHouseholdIncomeID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN LocationTaxonomyTree LtTownCity ON LtTownCity.LOCATION_ID = Consumers.LocationTaxonomyTreeID
	INNER JOIN LocationTaxonomyTree LtState ON LtState.LOCATION_ID = LtTownCity.ParentLocationID
	INNER JOIN LocationTaxonomyTree LtCountry ON LtCountry.LOCATION_ID = LtState.ParentLocationID
WHERE
	LtState.ParentLocationID IS NOT NULL -- Three levels up the tree, typically the format will be Country-State-TownCity
	AND Users.USER_ID = @UserID
UNION
SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	NULL AS CountryID,
	NULL AS StateID,
	NULL AS TownCityID,
	Consumers.OccupationSectorID,
	Consumers.EmploymentLevelID,
	Consumers.AnnualHouseholdIncomeID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
WHERE
	Consumers.LocationTaxonomyTreeID IS NULL -- haven't yet entered their geographical details --
	AND Users.USER_ID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserDetailsFromInstall]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserDetailsFromInstall]
(
@UserGUID nvarchar(50),
@Password nvarchar(50)
)

AS

SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	LtCountry.LocationName AS Country,
	NULL AS State,
	LtTownCity.LocationName AS TownCity,
	OccupationSector.SocioEconomicValue AS OccupationSector,
	EmploymentLevel.SocioEconomicValue AS EmploymentLevel,
	AnnualHouseholdIncome.SocioEconomicValue AS AnnualHouseholdIncome
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN LocationTaxonomyTree LtTownCity ON LtTownCity.LOCATION_ID = Consumers.LocationTaxonomyTreeID
	INNER JOIN LocationTaxonomyTree LtCountry ON LtCountry.LOCATION_ID = LtTownCity.ParentLocationID
	LEFT OUTER JOIN SocioEconomicStatus OccupationSector ON Consumers.OccupationSectorID = OccupationSector.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus EmploymentLevel ON Consumers.EmploymentLevelID = EmploymentLevel.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus AnnualHouseholdIncome ON Consumers.AnnualHouseholdIncomeID = AnnualHouseholdIncome.SOCIOECONOMICSTATUS_ID
WHERE
	LtCountry.ParentLocationID IS NULL -- Only two levels up the tree, the top of which is the country.
	AND Users.UserGUID = @UserGUID
	AND Users.UserPassword = @Password
UNION
SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	LtCountry.LocationName AS Country,
	LtState.LocationName AS State,
	LtTownCity.LocationName AS TownCity,
	OccupationSector.SocioEconomicValue AS OccupationSector,
	EmploymentLevel.SocioEconomicValue AS EmploymentLevel,
	AnnualHouseholdIncome.SocioEconomicValue AS AnnualHouseholdIncome
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN LocationTaxonomyTree LtTownCity ON LtTownCity.LOCATION_ID = Consumers.LocationTaxonomyTreeID
	INNER JOIN LocationTaxonomyTree LtState ON LtState.LOCATION_ID = LtTownCity.ParentLocationID
	INNER JOIN LocationTaxonomyTree LtCountry ON LtCountry.LOCATION_ID = LtState.ParentLocationID
	LEFT OUTER JOIN SocioEconomicStatus OccupationSector ON Consumers.OccupationSectorID = OccupationSector.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus EmploymentLevel ON Consumers.EmploymentLevelID = EmploymentLevel.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus AnnualHouseholdIncome ON Consumers.AnnualHouseholdIncomeID = AnnualHouseholdIncome.SOCIOECONOMICSTATUS_ID
WHERE
	LtState.ParentLocationID IS NOT NULL -- Three levels up the tree, typically the format will be Country-State-TownCity
	AND Users.UserGUID = @UserGUID
	AND Users.UserPassword = @Password
UNION
SELECT
	Users.Username,
	Users.UserPassword,
	Consumers.FirstName,
	Consumers.LastName,
	Consumers.Gender,
	Consumers.DOB,
	NULL AS CountryID,
	NULL AS StateID,
	NULL AS LtTownCity,
	OccupationSector.SocioEconomicValue AS OccupationSector,
	EmploymentLevel.SocioEconomicValue AS EmploymentLevel,
	AnnualHouseholdIncome.SocioEconomicValue AS AnnualHouseholdIncome
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	LEFT OUTER JOIN SocioEconomicStatus OccupationSector ON Consumers.OccupationSectorID = OccupationSector.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus EmploymentLevel ON Consumers.EmploymentLevelID = EmploymentLevel.SOCIOECONOMICSTATUS_ID
	LEFT OUTER JOIN SocioEconomicStatus AnnualHouseholdIncome ON Consumers.AnnualHouseholdIncomeID = AnnualHouseholdIncome.SOCIOECONOMICSTATUS_ID
WHERE
	Consumers.LocationTaxonomyTreeID IS NULL
	AND Users.UserGUID = @UserGUID
	AND Users.UserPassword = @Password
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserDemographicData]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserDemographicData]
(
@UserGUID nvarchar(50)
)

AS

SELECT
	-- TODO: make below dynamic where needed --
	'1.1' AS TaxonomyTree,	
	DATEDIFF(year, Consumers.DOB, GETDATE()) AS Age,
	Consumers.Gender,	
	'1.1' as SocioEconomicGroup
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
	LEFT OUTER JOIN LocationTaxonomyTree ON LocationTaxonomyTree.LOCATION_ID = Users.LocationID
WHERE
	Users.UserGUID = @UserGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserGUIDExists]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserGUIDExists]
(
@UserGUID nvarchar(50)
)

AS

SELECT 1 FROM Consumers INNER JOIN Users on Consumers.UserID = Users.USER_ID
WHERE Users.UserGUID = @UserGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserLogin]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserLogin]
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
		AND Users.USER_ID = @UserID;

END
GO
/****** Object:  Table [dbo].[SlideFolders]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SlideFolders](
	[SLIDEFOLDER_ID] [int] IDENTITY(1,1) NOT NULL,
	[PublisherID] [int] NOT NULL,
	[SlideFolderName] [nvarchar](50) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_SlideFolders_1] PRIMARY KEY CLUSTERED 
(
	[SLIDEFOLDER_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PCs]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PCs](
	[PC_ID] [int] IDENTITY(1,1) NOT NULL,
	[ConsumerID] [int] NULL,
	[PCGUID] [nvarchar](50) NULL,
	[MACAddress] [nvarchar](20) NULL,
	[PcName] [nvarchar](50) NULL,
	[MajorVersionNumber] [int] NULL,
	[MinorVersionNumber] [int] NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
	[MadeLastDirtyDate] [datetime] NULL,
	[bLinkedToClient] [bit] NOT NULL,
 CONSTRAINT [PK_PCs] PRIMARY KEY CLUSTERED 
(
	[PC_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [_dta_index_PCs_5_1394820031__K3_K2] ON [dbo].[PCs] 
(
	[PCGUID] ASC,
	[ConsumerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PcChannels]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PcChannels](
	[PcID] [int] NOT NULL,
	[ChannelID] [int] NOT NULL,
	[ChannelWeighting] [int] NOT NULL,
	[UserGUID] [nvarchar](50) NULL,
	[PCGUID] [nvarchar](50) NULL,
 CONSTRAINT [PK_PcChannels] PRIMARY KEY CLUSTERED 
(
	[PcID] ASC,
	[ChannelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_getChannelPrivacyStatus]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_getChannelPrivacyStatus]
(
	@UserID int,
	@ChannelID int
)
RETURNS nvarchar(10)
AS
BEGIN

	DECLARE @ReturnVal nvarchar(10);
	
	SELECT @ReturnVal = CASE WHEN EXISTS (SELECT 1 FROM Channels 
				WHERE Channels.CHANNEL_ID = @ChannelID 
				AND Channels.bLocked = 0) THEN 'Public'
				WHEN EXISTS(SELECT 1 FROM ChannelPasswordUnlock
				  WHERE ChannelPasswordUnlock.ChannelID = @ChannelID
				  AND ChannelPasswordUnlock.UserID = @UserID) THEN 'Unlocked'
				 ELSE 'Locked' END;
			 
	RETURN @ReturnVal;
	
END
GO
/****** Object:  Table [dbo].[Slides]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slides](
	[SLIDE_ID] [int] IDENTITY(1,1) NOT NULL,
	[SlideFolderID] [int] NOT NULL,
	[Filename] [nvarchar](255) NOT NULL,
	[FilenameExtension] [nvarchar](10) NULL,
	[FilenameNoPath] [nvarchar](255) NOT NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[SubDir] [char](1) NOT NULL,
	[SlideName] [nvarchar](50) NOT NULL,
	[Creator] [nvarchar](100) NULL,
	[Caption] [nvarchar](400) NULL,
	[ClickThroughURL] [nvarchar](255) NULL,
	[WebsiteURL] [nvarchar](255) NULL,
	[DisplayDuration] [float] NOT NULL,
	[Length] [int] NOT NULL,
	[ImagePath] [nvarchar](255) NULL,
	[ImagePathWinFS] [nvarchar](255) NULL,
	[ImageFilename] [nvarchar](255) NULL,
	[PlayerType] [nchar](20) NOT NULL,
	[PreviewType] [nvarchar](10) NOT NULL,
	[bLocked] [bit] NOT NULL,
	[UserGivenDate] [datetime] NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NOT NULL,
	[MadeDirtyLastDate] [datetime] NULL,
 CONSTRAINT [PK_Slides] PRIMARY KEY CLUSTERED 
(
	[SLIDE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[dp_getMachineGUIDExists]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getMachineGUIDExists]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

SELECT 1 FROM PCs 
INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE Users.UserGUID = @UserGUID
AND PCs.PCGUID = @MachineGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getIfPCExistsReturnGUID]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getIfPCExistsReturnGUID]
(
@MacAddress nvarchar(20),
@Username nvarchar(100),
@UserGUID nvarchar(50) OUTPUT,
@MachineGUID nvarchar(50) OUTPUT
)

AS

IF EXISTS (SELECT 1 FROM PCs WHERE MACAddress = @MacAddress)
BEGIN

	SELECT
		@MachineGUID = PCs.PCGUID
	FROM
		PCs
	WHERE
		PCs.MACAddress = @MacAddress;

END

SELECT
	@UserGUID = Users.UserGUID
FROM
	Users
WHERE
	Users.Username = @Username;
GO
/****** Object:  StoredProcedure [dbo].[dp_getNoPagesPerChannelPerCategory]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getNoPagesPerChannelPerCategory]
(
@CategoryID int
)

AS

DECLARE @CTT nvarchar(4000);
DECLARE @TempT TABLE (OrderNo int, CTTNode int);

DECLARE @CTTLevel1 int;
DECLARE @CTTLevel2 int;
DECLARE @CTTLevel3 int;
DECLARE @CTTLevel4 int;
DECLARE @CTTLevel5 int;
DECLARE @CTTLevel6 int;
DECLARE @CTTLevel7 int;
DECLARE @CTTLevel8 int;
DECLARE @CTTLevel9 int;
DECLARE @CTTLevel10 int;

SELECT 
	@CTT = Categories.CTT 
FROM 
	Categories 
WHERE 
	Categories.CATEGORY_ID = @CategoryID

-- split the CTT into a temp table --
INSERT INTO @TempT (OrderNo, CTTNode)
SELECT IDENTITY_ID, Data
FROM fn_SplitCDLToTableInt(@CTT, '.');

SELECT @CTTLevel1 = CTTNode FROM @TempT WHERE OrderNo = 1;
SELECT @CTTLevel2 = CTTNode FROM @TempT WHERE OrderNo = 2;
SELECT @CTTLevel3 = CTTNode FROM @TempT WHERE OrderNo = 3;
SELECT @CTTLevel4 = CTTNode FROM @TempT WHERE OrderNo = 4;
SELECT @CTTLevel5 = CTTNode FROM @TempT WHERE OrderNo = 5;
SELECT @CTTLevel6 = CTTNode FROM @TempT WHERE OrderNo = 6;
SELECT @CTTLevel7 = CTTNode FROM @TempT WHERE OrderNo = 7;
SELECT @CTTLevel8 = CTTNode FROM @TempT WHERE OrderNo = 8;
SELECT @CTTLevel9 = CTTNode FROM @TempT WHERE OrderNo = 9;
SELECT @CTTLevel10 = CTTNode FROM @TempT WHERE OrderNo = 10;

SELECT 
	CONVERT(int, CEILING(CONVERT(float, COUNT(Channels.CHANNEL_ID)) / CONVERT(float, 12))) AS NoPages
FROM 
	Channels INNER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
WHERE
	(@CTTLevel1 IS NULL OR Categories.CTTLevel1 = @CTTLevel1)
	AND (@CTTLevel2 IS NULL OR Categories.CTTLevel2 = @CTTLevel2)
	AND (@CTTLevel3 IS NULL OR Categories.CTTLevel3 = @CTTLevel3)
	AND (@CTTLevel4 IS NULL OR Categories.CTTLevel4 = @CTTLevel4)
	AND (@CTTLevel5 IS NULL OR Categories.CTTLevel5 = @CTTLevel5)
	AND (@CTTLevel6 IS NULL OR Categories.CTTLevel6 = @CTTLevel6)
	AND (@CTTLevel7 IS NULL OR Categories.CTTLevel7 = @CTTLevel7)
	AND (@CTTLevel8 IS NULL OR Categories.CTTLevel8 = @CTTLevel8)
	AND (@CTTLevel9 IS NULL OR Categories.CTTLevel9 = @CTTLevel9)
	AND (@CTTLevel10 IS NULL OR Categories.CTTLevel10 = @CTTLevel10);
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCsToLinkByGUID]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCsToLinkByGUID]

(
@UserGUID nvarchar(50)
)

AS

SELECT 
	PCs.PC_ID,
	PCs.PcName
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
	INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.UserGUID = @UserGUID
	AND PCs.bLinkedToClient = 0;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCsToLinkByEmail]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCsToLinkByEmail]

(
@EmailAddress nvarchar(100)
)

AS

SELECT 
	PCs.PC_ID,
	PCs.PcName
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
	AND Consumers.EmailAddress = @EmailAddress	
	AND PCs.bLinkedToClient = 0;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCListNotRegistered]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCListNotRegistered]
(
@PCProfileToken nvarchar(50)
)

AS

SELECT
	PC_ID,
	PcName,
	bLinkedToClient
FROM PCs
WHERE
	PCs.PCGUID = @PCProfileToken;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCList]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCList]
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
	AND Consumers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserHasRealPCs]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserHasRealPCs]

(@UserID int)

AS

SELECT 1 FROM PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE Consumers.UserID = @UserID
AND PCs.bLinkedToClient = 1;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelThumbnailsToDelete]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelThumbnailsToDelete]

(@UserID int)

AS

SELECT 
	REPLACE(Channels.ImagePath, '/', '\') AS ImagePath
FROM
	Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelSimple]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelSimple]
(
@ChannelID int
)
AS

SELECT
	CHANNEL_ID,
	ChannelDescription,
	NoContent,
	NoFollowers,
	Channels.AddDate,
	DisplayName
FROM
	Channels,
	Publishers
WHERE
	Channels.CHANNEL_ID = @ChannelID
	AND Publishers.PUBLISHER_ID = Channels.PublisherID
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelInfo]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelInfo]

(@UserID int, @ChannelID int)

AS

IF (EXISTS (SELECT 1 FROM Channels WHERE CHANNEL_ID = @ChannelID AND bLocked = 1)
	AND EXISTS (SELECT 1 FROM ChannelPasswordUnlock WHERE UserID = @UserID AND ChannelID = @ChannelID))
	OR EXISTS (SELECT 1 FROM Channels WHERE CHANNEL_ID = @ChannelID AND bLocked = 0)
BEGIN

	SELECT
		Channels.CHANNEL_ID,
		Channels.ChannelGUID,
		Channels.ChannelName
	FROm
		Channels
	WHERE
		Channels.CHANNEL_ID = @ChannelID;
END
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelPropertiesByChannelID]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelPropertiesByChannelID]
(
@UserID int,
@ChannelID int
)

AS

SELECT
	Channels.CHANNEL_ID,
	Categories.CATEGORY_ID,
	Categories.CategoryName,
	Channels.ChannelName,
	Channels.ChannelDescription,
	Channels.ChannelLongDescription,
	Channels.Keywords,
	Channels.bLocked,
	Channels.ChannelPassword,
	Channels.bAcceptPasswordRequests,
	CAST(CASE WHEN EXISTS (SELECT 1 FROM ChannelPasswordUnlock WHERE ChannelPasswordUnlock.ChannelID = @ChannelID) THEN 1 ELSE 0 END AS BIT) AS bHasAuthorizedUsers
FROM
	(Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID)
	 LEFT OUTER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
WHERE
	Channels.CHANNEL_ID = @ChannelID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelCreatorDetailsForPasswordRequest]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelCreatorDetailsForPasswordRequest]

(@ChannelID int)

AS

SELECT
	Channels.ChannelName,
	Publishers.FirstName,
	Publishers.EmailAddress
FROM
	Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Channels.CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAllFoldersByUserID]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAllFoldersByUserID]
(
@UserID int
)

AS

SELECT
	1 AS OrderNumber,
	AssetContentFolders.ASSETCONTENTFOLDER_ID AS FolderID,
	AssetContentFolders.AssetContentFolderName AS FolderName
FROM
	AssetContentFolders INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE 
	Publishers.UserID = @UserID
UNION
SELECT
	2 AS OrderNumber,
	SlideFolders.SLIDEFOLDER_ID AS FolderID,
	SlideFolders.SlideFolderName  AS FolderName
FROM
	SlideFolders INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE  
	Publishers.UserID = @UserID
UNION
SELECT
	3 AS OrderNumber,
	Channels.CHANNEL_ID AS FolderID,
	Channels.ChannelName AS FolderName
FROM
	Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE 
	Publishers.UserID = @UserID
ORDER BY
	OrderNumber,
	FolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentFoldersByUserID]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentFoldersByUserID]
(
@UserID int
)

AS

SELECT
	ASSETCONTENTFOLDER_ID,
	AssetContentFolderName
FROM
	AssetContentFolders,
	Publishers
WHERE
	AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelThumbnail]    Script Date: 04/04/2011 13:38:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelThumbnail]
(
@UserID int,
@ChannelID int,
@ThumbnailGUID nvarchar(50),
@ChannelGUIDSuffix char(1) OUTPUT,
@OldChannelThumbnailPath nvarchar(255) OUTPUT
)

AS

SELECT
	@ChannelGUIDSuffix = ChannelGUIDSuffix,
	@OldChannelThumbnailPath = ImagePath
FROM
	Channels
WHERE
	Channels.CHANNEL_ID = @ChannelID;
	
UPDATE
	Channels
SET
	ImagePath = @ChannelGUIDSuffix + '/' + @ThumbnailGUID + '.jpg'
WHERE
	Channels.CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editSlideFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editSlideFolder]
(
@UserID int,
@SlideFolderID int,
@SlideFolderName nvarchar(50)
)

AS

IF NOT EXISTS (SELECT 1 FROM SlideFolders, Publishers WHERE PublisherID = PUBLISHER_ID AND UserID = @UserID)
RETURN;

UPDATE 
	SlideFolders
SET 
	SlideFolderName = @SlideFolderName,
	EditDate = GETDATE()
WHERE
	SLIDEFOLDER_ID = @SlideFolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editPCMakeDirtyGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editPCMakeDirtyGUID]

(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

UPDATE
	PCs
SET
	MadeLastDirtyDate = GETDATE()
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
	INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	PCs.PCGUID = @MachineGUID
	AND Users.UserGUID = @UserGUID
GO
/****** Object:  StoredProcedure [dbo].[dp_editPCMakeDirtyEmailAndGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editPCMakeDirtyEmailAndGUID]

(
@EmailAddress nvarchar(100),
@MachineGUID nvarchar(50)
)

AS

UPDATE
	PCs
SET
	MadeLastDirtyDate = GETDATE()
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
	INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	PCs.PCGUID = @MachineGUID
	AND Users.Username = @EmailAddress;
GO
/****** Object:  StoredProcedure [dbo].[dp_editPCMakeClean]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editPCMakeClean]
(
@PCID int
)

AS

UPDATE
	PCs
SET
	MadeLastDirtyDate = NULL
WHERE
	PC_ID = @PCID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editPCLinkToClientYes]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editPCLinkToClientYes]

(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

DECLARE @ConsumerID int;

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users on Consumers.UserID = Users.USER_ID
WHERE
	Users.UserGUID = @UserGUID;

IF EXISTS (SELECT 1 FROM PCs WHERE PCs.PCGUID = @MachineGUID)

	UPDATE
		PCs
	SET
		bLinkedToClient = 1
	FROm
		PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
	WHERE
		PCs.PCGUID = @MachineGUID
		AND Consumers.CONSUMER_ID = @ConsumerID
		
ELSE

INSERT INTO PCs(ConsumerID, PCGUID, PcName, bLinkedToClient)
VALUES (@ConsumerID, @MachineGUID, 'New PC', 1);
GO
/****** Object:  StoredProcedure [dbo].[dp_editPC]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editPC]
(
@UserID int,
@PCID int,
@PCName nvarchar(50)
)

AS

DECLARE @ConsumerID int;

SELECT @ConsumerID = CONSUMER_ID
FROM Consumers
WHERE UserID = @UserID;

UPDATE 
	PCs
SET 
	PcName = @PCName,
	EditDate = GETDATE()
WHERE 
	PCs.PC_ID = @PCID 
	AND PCs.ConsumerID = @ConsumerID;
GO
/****** Object:  StoredProcedure [dbo].[dp_addSlideFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addSlideFolder]
(
@UserID int,
@SlideFolderName nvarchar(50)
)

AS

DECLARE @PublisherID int;
DECLARE @id int;

SELECT
	@PublisherID = PUBLISHER_ID
FROM
	Publishers
WHERE
	UserID = @UserID;

INSERT INTO SlideFolders(PublisherID, SlideFolderName)
VALUES (@PublisherID, @SlideFolderName);

RETURN SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[dp_addPublisherConsumer]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPublisherConsumer]
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
INSERT INTO Users (UserGUID, Username, UserPassword, LastLoginDate, AccountTypeID)
VALUES (@UserGUID, @EmailAddress, @Password, GETDATE(), @NormalAccountTypeID); 

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
/****** Object:  StoredProcedure [dbo].[dp_createPcIfNotExists]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[dp_createPcIfNotExists]

(@UserGUID nvarchar(50), 
@MachineName nvarchar(50),
@MacAddress nvarchar(20), 
@MajorVersionNumber int,
@MinorVersionNumber int,
@MachineGUID nvarchar(50) OUTPUT)

AS

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

	INSERT INTO PCs(ConsumerID, PCGUID, MACAddress, PcName, MajorVersionNumber, MinorVersionNumber, bLinkedToClient, AddDate)
	VALUES (@ConsumerID, @MachineGUID, @MacAddress, @MachineName, @MajorVersionNumber, @MinorVersionNumber, 1, getdate());
END
ELSE
	SELECT 
		@MachineGUID = PCs.PCGUID
	FROM
		PCs
	WHERE
		PCs.MACAddress = @MacAddress;
GO
/****** Object:  StoredProcedure [dbo].[dp_addUserAddPCLinkPC]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addUserAddPCLinkPC]
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
	@MACAddress nvarchar(20),
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
/****** Object:  StoredProcedure [dbo].[dp_addUnlockStream]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addUnlockStream]
(
@UserID int,
@ChannelID int,
@ChannelPassword nvarchar(10)
)

AS

DECLARE @bUnlocked bit;

IF EXISTS (SELECT 1 FROM Channels WHERE Channels.CHANNEL_ID = @ChannelID 
			AND Channels.ChannelPassword = @ChannelPassword)
BEGIN

	INSERT INTO ChannelPasswordUnlock(UserID, ChannelID) VALUES (@UserID, @ChannelID);
	
	SET @bUnlocked = 1;
END
ELSE
	SET @bUnlocked = 0;
	
SELECT @bUnlocked AS Unlocked;
GO
/****** Object:  StoredProcedure [dbo].[dp_addTempPcProfileUnregistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addTempPcProfileUnregistered]

(@PCProfileToken nvarchar(50))

AS

IF NOT EXISTS (SELECT 1 FROM PCs WHERE PCGUID = @PCProfileToken)
	INSERT INTO PCs(PcName, PCGUID, bLinkedToClient) VALUES ('Temporary PC Profile 1', @PCProfileToken, 0);
GO
/****** Object:  StoredProcedure [dbo].[dp_addTempPcProfileRegistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addTempPcProfileRegistered]

(@UserID int)

AS

DECLARE @ConsumerID int;

SELECT @ConsumerID = Consumers.CONSUMER_ID FROM Consumers WHERE Consumers.UserID = @UserID;

IF NOT EXISTS (SELECT 1 FROM PCs 
				INNER JOIN Consumers on PCs.ConsumerID = Consumers.CONSUMER_ID 
				WHERE Consumers.UserID = @UserID)
				
	
	INSERT INTO PCs(PcName, ConsumerID, bLinkedToClient) VALUES ('Temporary PC Profile 1', @ConsumerID, 0);
GO
/****** Object:  StoredProcedure [dbo].[dp_editAssetContentFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editAssetContentFolder]
(
@UserID int,
@FolderID int,
@FolderName nvarchar(50)
)

AS

IF NOT EXISTS (SELECT 1 FROM AssetContentFolders, Publishers WHERE PublisherID = PUBLISHER_ID AND UserID = @UserID)
RETURN;

UPDATE 
	AssetContentFolders
SET 
	AssetContentFolderName = @FolderName
WHERE
	ASSETCONTENTFOLDER_ID = @FolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelMakeClean]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelMakeClean]

(
@ChannelID int
)

AS

UPDATE
	Channels
SET
	MadeDirtyLastDate = NULL
WHERE
	CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelCategory]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelCategory]

(
@UserID int,
@ChannelID int,
@CategoryID int
)

AS

UPDATE
	Channels
SET
	CategoryID = @CategoryID
FROM
	Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Channels.CHANNEL_ID = @ChannelID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  Table [dbo].[AdvertContents]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertContents](
	[ADVERTCONTENT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AdvertID] [int] NOT NULL,
	[AdvertContentFilename] [nvarchar](255) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_AdvertContents] PRIMARY KEY CLUSTERED 
(
	[ADVERTCONTENT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCLinkToClientYes]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCLinkToClientYes]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@PCName nvarchar(100)
)

AS

DECLARE @ConsumerID int;

SELECT
	@ConsumerID = Consumers.CONSUMER_ID
FROM
	Consumers INNER JOIN Users ON Consumers.UserID = Users.USER_ID
WHERE
	Users.UserGUID = @UserGUID;

INSERT INTO PCs(ConsumerID, PCGUID, PcName, bLinkedToClient)
VALUES (@ConsumerID, @MachineGUID, @PCName, 1);
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCByUserGUIDMachineGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCByUserGUIDMachineGUID]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@MachineName nvarchar(50),
@MACAddress nvarchar(50),
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
/****** Object:  StoredProcedure [dbo].[dp_addPCByGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCByGUID]
(
@EmailAddress nvarchar(50),
@MachineGUID nvarchar(50),
@MACAddress nvarchar(20),
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
/****** Object:  StoredProcedure [dbo].[dp_addPC]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPC]
(
@UserID int,
@PCName nvarchar(50)
)

AS

DECLARE @ConsumerID int;
DECLARE @PCGUID nvarchar(50);

SELECT @ConsumerID = CONSUMER_ID
FROM Consumers
WHERE UserID = @UserID;

SET @PCGUID = CONVERT(nvarchar(48), NEWID()) + '_' + SUBSTRING(@PCName, 1, 1);

INSERT INTO PCs(ConsumerID,  PcName, bLinkedToClient)
VALUES (@ConsumerID, @PCName, 0);

RETURN SCOPE_IDENTITY();
GO
/****** Object:  Table [dbo].[AdvertsAdvertTaxonomyTrees]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdvertsAdvertTaxonomyTrees](
	[AdvertID] [int] NOT NULL,
	[AdvertTaxonomyTreeID] [int] NOT NULL,
 CONSTRAINT [PK_AdvertsAdvertTaxonomyTrees] PRIMARY KEY CLUSTERED 
(
	[AdvertID] ASC,
	[AdvertTaxonomyTreeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_addChannel]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addChannel]
(
@UserID int,
@CategoryID int = null,
@ChannelName nvarchar(50),
@ChannelDescription nvarchar(200) = null,
@ChannelLongDescription nvarchar(4000) = null,
@Keywords nvarchar(4000) = null,
@bLocked bit,
@Password nvarchar(10) = null,
@bAcceptPasswordRequests bit,
@ChannelGUID nvarchar(50) OUTPUT
)

AS

DECLARE @PublisherID int;
DECLARE @ChannelGUIDSuffix char(1);

SET @ChannelGUID = CONVERT(nvarchar(38), NEWID()) + '_' + SUBSTRING(@ChannelName, 1, 1);
SET @ChannelGUIDSuffix = (substring(@ChannelGUID,len(@ChannelGUID),(1)))

SELECT
	@PublisherID = Publishers.PUBLISHER_ID
FROM
	Publishers
WHERE
	Publishers.UserID = @UserID
		
INSERT INTO Channels(CategoryID, ChannelName, PublisherID, ChannelGUID, ChannelDescription, ChannelLongDescription, Keywords, bLocked, ChannelPassword, bAcceptPasswordRequests, bHasDefaultThumbnail, ImagePath, NoContent, NoFollowers)
VALUES (@CategoryID, @ChannelName, @PublisherID, @ChannelGUID, @ChannelDescription, @ChannelLongDescription, @Keywords, @bLocked, @Password, @bAcceptPasswordRequests, 1, @ChannelGUIDSuffix + '/' + @ChannelGUID + '.jpg', 0, 0)

RETURN SCOPE_IDENTITY();
GO
/****** Object:  StoredProcedure [dbo].[dp_addAssetContentFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addAssetContentFolder]
(
@UserID int,
@FolderName nvarchar(50)
)

AS

DECLARE @PublisherID int;

SELECT
	@PublisherID = PUBLISHER_ID
FROM
	Publishers
WHERE
	UserID = @UserID;

INSERT INTO AssetContentFolders(PublisherID, AssetContentFolderName)
VALUES (@PublisherID, @FolderName);

RETURN SCOPE_IDENTITY();
GO
/****** Object:  Table [dbo].[AssetContents]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AssetContents](
	[ASSETCONTENT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AssetContentFolderID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Caption] [nvarchar](400) NULL,
	[GUID] [nvarchar](50) NOT NULL,
	[Filename] [nvarchar](255) NOT NULL,
	[FilenameNoPath] [nvarchar](255) NOT NULL,
	[FilenameExtension] [nvarchar](10) NOT NULL,
	[ImagePath] [nvarchar](255) NOT NULL,
	[ImagePathWinFS] [nvarchar](255) NOT NULL,
	[SubDir] [char](1) NULL,
	[ImageName] [nvarchar](100) NULL,
	[UserGivenDate] [datetime] NULL,
	[URL] [nvarchar](255) NULL,
	[Creator] [nvarchar](100) NULL,
	[DisplayDuration] [float] NOT NULL,
	[Length] [int] NOT NULL,
	[PreviewType] [nvarchar](10) NOT NULL,
	[AddDate] [datetime] NOT NULL,
	[EditDate] [datetime] NULL,
 CONSTRAINT [PK_AssetContents] PRIMARY KEY CLUSTERED 
(
	[ASSETCONTENT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChannelUserPrivacy]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChannelUserPrivacy](
	[ChannelID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_ChannelUserPrivacy] PRIMARY KEY CLUSTERED 
(
	[ChannelID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChannelsSlides]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChannelsSlides](
	[CHANNELSSLIDE_ID] [int] IDENTITY(1,1) NOT NULL,
	[ChannelID] [int] NOT NULL,
	[SlideID] [int] NOT NULL,
	[ClickThroughURL] [nvarchar](255) NULL,
	[DisplayDuration] [float] NOT NULL,
	[Schedule] [nvarchar](max) NULL,
	[PresentationConvertedSchedule] [nvarchar](4000) NULL,
 CONSTRAINT [PK_ChannelsSlides_1] PRIMARY KEY NONCLUSTERED 
(
	[CHANNELSSLIDE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE CLUSTERED INDEX [IX_ChannelsSlides] ON [dbo].[ChannelsSlides] 
(
	[ChannelID] ASC,
	[SlideID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[dp_addAssetContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addAssetContent]
(
@UserID int,
@AssetContentFolderID int,
@Name nvarchar(100),
@GUID nvarchar(50),
@Caption nvarchar(400) = null,
@Creator nvarchar(100) = null,
@Filename nvarchar(255),
@FilenameNoPath nvarchar(255),
@FilenameExtension nvarchar(10),
@ImagePath nvarchar(255),
@ImagePathWinFS nvarchar(255),
@SubDir char(1),
@ImageName nvarchar(100),
@UserGivenDate datetime = null,
@URL nvarchar(255) = null,
@DisplayDuration float,
@Length int,
@PreviewType nvarchar(10)
)

AS

-- check if user has access --
IF NOT EXISTS (SELECT 1 
				FROM 
					AssetContentFolders INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE 
					Publishers.UserID = @UserID
					AND AssetContentFolders.ASSETCONTENTFOLDER_ID = @AssetContentFolderID)
	RETURN -1;

UPDATE 
	Publishers
SET
	UsedBytes = UsedBytes + @Length
WHERE
	Publishers.UserID = @UserID;
	
INSERT INTO AssetContents (AssetContentFolderID, GUID, Name, Caption, Creator, Filename, FilenameNoPath, FilenameExtension, ImagePath, ImagePathWinFS, SubDir, ImageName, UserGivenDate, URL, DisplayDuration, Length, PreviewType)
VALUES (@AssetContentFolderID, @GUID, @Name, @Caption, @Creator, @Filename, @FilenameNoPath, @FilenameExtension, @ImagePath, @ImagePathWinFS, @SubDir, @ImageName, @UserGivenDate, @URL, @DisplayDuration, @Length, @PreviewType)
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannel]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannel]
(
@UserID int,
@ChannelID int,
@CategoryID int = null,
@ChannelName nvarchar(50),
@ChannelDescription nvarchar(200) = null,
@ChannelLongDescription nvarchar(4000) = null,
@Keywords nvarchar(4000) = null,
@bLocked bit,
@Password nvarchar(10) = null,
@bAcceptPasswordRequests bit,
@ChannelPrivacyOptions nvarchar(40)
)

AS

DECLARE @OldCategoryID int;
DECLARE @MakeChannelDirty bit;
DECLARE @bExistingLocked bit;

SELECT 
	@OldCategoryID = Channels.CategoryID,
	@bExistingLocked = Channels.bLocked
FROM
	Channels
WHERE
	Channels.CHANNEL_ID = @ChannelID;

UPDATE
	Channels
SET
	CategoryID = @CategoryID,
	ChannelName = @ChannelName,
	ChannelDescription = @ChannelDescription,
	ChannelLongDescription = @ChannelLongDescription,
	Keywords = @Keywords,
	bLocked = @bLocked,
	ChannelPassword = @Password,
	bAcceptPasswordRequests = @bAcceptPasswordRequests,
	EditDate = GETDATE()
FROM
	Channels,
	Publishers
WHERE
	Channels.PublisherID = Publishers.PUBLISHER_ID
	AND Publishers.UserID = @UserID
	AND Channels.CHANNEL_ID = @ChannelID;
	
-- channel privacy options: what to do with the followers / authorized users when privacy settings have changed --

-- Channel was private, remains private and all authorized users are to be unauthorized (implies channel password change) -- 
IF (@bExistingLocked = 1 AND @bLocked = 1 AND @ChannelPrivacyOptions = 'UnauthorizeExistingAuthorizedUsers')
BEGIN

	DELETE FROM ChannelPasswordUnlock WHERE ChannelPasswordUnlock.ChannelID = @ChannelID;

END

-- Channel was public and will become private --
IF (@bExistingLocked = 0 AND @bLocked = 1)
BEGIN

	-- channel owner wants to unauthorize users previously 
	IF (@ChannelPrivacyOptions = 'UnauthorizeExistingAuthorizedUsers')
	BEGIN
	
		DELETE FROM ChannelPasswordUnlock WHERE ChannelPasswordUnlock.ChannelID = @ChannelID;
	
	END
	
	-- all followers to be authorized
	IF (@ChannelPrivacyOptions = 'AuthorizeAllFollowers')
	BEGIN
	
		INSERT INTO ChannelPasswordUnlock (ChannelID, UserID)
		(SELECT DISTINCT @ChannelID, Consumers.UserID
			FROM PcChannels 
				INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
				INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				WHERE PcChannels.ChannelID = @ChannelID)
	
	END

END
GO
/****** Object:  StoredProcedure [dbo].[dp_editAssetContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editAssetContent]
(
@UserID int,
@AssetContentID int,
@Name nvarchar(100),
@Creator nvarchar(100) = null,
@Caption nvarchar(400) = null,
@UserGivenDate datetime = null,
@URL nvarchar(255) = null,
@DisplayDuration float
)

AS

UPDATE
	AssetContents
SET
	Name = @Name,
	Creator = @Creator,
	Caption = @Caption,
	UserGivenDate = @UserGivenDate,
	URL = @URL,
	DisplayDuration = @DisplayDuration
FROM
	AssetContents,
	AssetContentFolders,
	Publishers
WHERE
	AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	AND AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
	AND Publishers.UserID = @UserID
	AND AssetContents.ASSETCONTENT_ID = @AssetContentID;
GO
/****** Object:  StoredProcedure [dbo].[dp_addSubscriptionByMachineGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addSubscriptionByMachineGUID]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@ChannelID int,
@ChannelWeightingUnnormalized int
)

AS

DECLARE @PCID int;

SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;
	
INSERT INTO PcChannels(ChannelID, PCGUID, UserGUID, ChannelWeighting, PcID)
VALUES (@ChannelID, @MachineGUID, @UserGUID, @ChannelWeightingUnnormalized, @PCID);

UPDATE
	PCs
SET
	PCs.MadeLastDirtyDate = GETDATE()
WHERE
	PCs.PCGUID = @MachineGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_addStreamByMacAddress]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addStreamByMacAddress]
(
@MACAddress nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS

DECLARE @PCGUID nvarchar(50);
DECLARE @PCID int;
DECLARE @UserGUID nvarchar(50);
DECLARE @ChannelID int;

SELECT
	@UserGUID = Users.UserGUID
FROM
	Users INNER JOIN Consumers ON Users.USER_ID = Consumers.UserID
	INNER JOIN PCs ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	PCs.MACAddress = @MACAddress;
	
SELECT
	@PCID = PCs.PC_ID,
	@PCGUID = PCs.PCGUID
FROM
	PCs
WHERE
	PCs.MACAddress = @MACAddress;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;
	
IF NOT EXISTS (SELECT 1 FROM PcChannels WHERE PcChannels.PcID = @PCID AND PcChannels.ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels(ChannelID, ChannelWeighting, PCGUID, PcID, UserGUID)
	VALUES (@ChannelID, @ChannelWeighting, @PCGUID, @PCID, @UserGUID);

END
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCStreamNotRegistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCStreamNotRegistered]

(
@PCID int,
@ChannelID int
)

AS

-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM Pcs WHERE ConsumerID IS NULL AND PC_ID = @PCID)
RETURN;
	
INSERT INTO PcChannels(PcID, ChannelID) VALUES (@PcID, @ChannelID);
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCStreamByUserGuidMachineGUIDChannelGUIDNoLinkNoDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCStreamByUserGuidMachineGUIDChannelGUIDNoLinkNoDirty]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS 

DECLARE @UserID int;
DECLARE @PCID int;
DECLARE @ChannelID int;

SELECT
	@UserID = Users.USER_ID
FROM
	Users
WHERE
	Users.UserGUID = @UserGUID;
	
IF (@@ROWCOUNT<>1)
	RETURN -1;
	
SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;
	
	IF (@@ROWCOUNT<>1)
	RETURN -50;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;	
	
IF (@@ROWCOUNT<>1)
	RETURN 2;
	
-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM 
				PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				INNER JOIN Users ON Consumers.UserID = Users.USER_ID
			  WHERE
				PCs.PC_ID = @PCID
				AND Users.USER_ID = @UserID)
RETURN -4;


IF NOT EXISTS(SELECT 1 FROM PcChannels WHERE PcID = @PCID AND ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels (PcID, ChannelID, UserGUID, PCGUID)
	VALUES (@PCID, @ChannelID, @UserGUID, @MachineGUID);

	UPDATE
		Channels
	SET
		NoFollowers = NoFollowers + 1
	WHERE
		Channels.CHANNEL_ID = @ChannelID;
END
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCStreamByGUIDsNoLinkNoDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCStreamByGUIDsNoLinkNoDirty]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS 

DECLARE @UserID int;
DECLARE @PCID int;
DECLARE @ChannelID int;

SELECT
	@UserID = Users.USER_ID
FROM
	Users
WHERE
	Users.UserGUID = @UserGUID;
	
IF (@@ROWCOUNT<>1)
	RETURN -1;
	
SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;
	
	IF (@@ROWCOUNT<>1)
	RETURN -50;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;	
	
IF (@@ROWCOUNT<>1)
	RETURN -60;
	
-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM 
				PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				INNER JOIN Users ON Consumers.UserID = Users.USER_ID
			  WHERE
				PCs.PC_ID = @PCID
				AND Users.USER_ID = @UserID)
RETURN -4;

IF NOT EXISTS(SELECT 1 FROM PcChannels WHERE PcID = @PCID AND ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels (PcID, ChannelID, UserGUID, PCGUID)
	VALUES (@PCID, @ChannelID, @UserGUID, @MachineGUID);

	UPDATE
		Channels
	SET
		NoFollowers = NoFollowers + 1
	WHERE
		Channels.CHANNEL_ID = @ChannelID;
END
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCStreamByEmailAndGUIDsNoLinkNoDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCStreamByEmailAndGUIDsNoLinkNoDirty]
(
@EmailAddress nvarchar(100),
@MachineGUID nvarchar(50),
@ChannelGUID nvarchar(50),
@ChannelWeighting int
)

AS 

DECLARE @UserID int;
DECLARE @UserGUID nvarchar(50);
DECLARE @PCID int;
DECLARE @ChannelID int;

SELECT
	@UserID = Users.USER_ID,
	@UserGUID = Users.UserGUID
FROM
	Users
WHERE
	Users.Username = @EmailAddress;
	
IF (@@ROWCOUNT<>1)
	RETURN -1;
	
SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.PCGUID = @MachineGUID;
	
	IF (@@ROWCOUNT<>1)
	RETURN -50;

SELECT
	@ChannelID = Channels.CHANNEL_ID
FROM
	Channels
WHERE
	Channels.ChannelGUID = @ChannelGUID;	
	
IF (@@ROWCOUNT<>1)
	RETURN -60;
	
-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM 
				PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				INNER JOIN Users ON Consumers.UserID = Users.USER_ID
			  WHERE
				PCs.PC_ID = @PCID
				AND Users.USER_ID = @UserID)
RETURN -4;


IF NOT EXISTS(SELECT 1 FROM PcChannels WHERE PcID = @PCID AND ChannelID = @ChannelID)
BEGIN

	INSERT INTO PcChannels (PcID, ChannelID, UserGUID, PCGUID)
	VALUES (@PCID, @ChannelID, @UserGUID, @MachineGUID);

	UPDATE
		Channels
	SET
		NoFollowers = NoFollowers + 1
	WHERE
		Channels.CHANNEL_ID = @ChannelID;
END
GO
/****** Object:  StoredProcedure [dbo].[dp_addPCStream]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addPCStream]
(
@UserID int,
@PCID int,
@ChannelID int
)

AS 

DECLARE @UserGUID nvarchar(50);
DECLARE @MachineGUID nvarchar(50);

-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM Pcs, Consumers WHERE ConsumerID = CONSUMER_ID AND UserID = @UserID AND PCs.PC_ID = @PCID)
RETURN;

IF NOT EXISTS(SELECT 1 FROM PcChannels WHERE PcID = @PCID AND ChannelID = @ChannelID)
BEGIN
	
	SELECT @UserGUID = Users.UserGUID FROM Users WHERE Users.USER_ID = @UserID;
	SELECT @MachineGUID = PCs.PCGUID FROM PCs WHERE PCs.PC_ID = @PCID;
	
	INSERT INTO PcChannels (PcID, ChannelID, UserGUID, PCGUID)
	VALUES (@PCID, @ChannelID, @UserGUID, @MachineGUID);
	
	IF EXISTS (SELECT 1 FROM PCs WHERE PC_ID = @PCID AND bLinkedToClient = 1)
	BEGIN
	
		UPDATE
			Channels
		SET
			NoFollowers = NoFollowers + 1
		WHERE
			Channels.CHANNEL_ID = @ChannelID;

		UPDATE
			PCs
		SET
			MadeLastDirtyDate = GETDATE()
		WHERE
			PC_ID = @PCID;
								
	END
END
GO
/****** Object:  StoredProcedure [dbo].[dp_addSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addSlide]
(
	@UserID int,
	@SlideFolderID int,
	@AssetContentID int,
	@GUID nvarchar(50),
	@AssetFilename nvarchar(255) OUTPUT,
	@SubDir char(1) OUTPUT,
	@SlideImagePathWinFS nvarchar(255) OUTPUT,
	@AssetFilenameExtension nvarchar(10) OUTPUT,
	@SlideFilenameWithPath nvarchar(100) OUTPUT,
	@SlideFilenameNoPath nvarchar(100) OUTPUT,
	@AssetImagePathWinFS nvarchar(255) OUTPUT
)

AS

DECLARE @AssetFilenameNoPath nvarchar(100);
DECLARE @SlideName nvarchar(50);
DECLARE @Creator nvarchar(100);
DECLARE @Caption nvarchar(100);
DECLARE @UserGivenDate datetime;
DECLARE @PlayerType nvarchar(20);
DECLARE @Length nvarchar(20);
DECLARE @URL nvarchar(255);
DECLARE @PreviewType nvarchar(10);
DECLARE @Duration float;
DECLARE @ImageName nvarchar(100);
DECLARE @AssetImageName nvarchar(100);

IF NOT EXISTS (SELECT 1 
				FROM SlideFolders INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID 
				WHERE Publishers.UserID = @UserID
				AND SlideFolders.SLIDEFOLDER_ID = @SlideFolderID)	
	RETURN;

SELECT
	@SlideName = AssetContents.Name,
	@AssetFilename = AssetContents.Filename,
	@Creator = AssetContents.Creator,
	@Caption = AssetContents.Caption,
	@URL = AssetContents.URL,
	@PreviewType = AssetContents.PreviewType,
	@Duration = AssetContents.DisplayDuration,
	@AssetFilenameNoPath = AssetContents.FilenameNoPath,
	@AssetFilenameExtension = AssetContents.FilenameExtension,
	@SubDir = AssetContents.SubDir,
	@AssetImageName = AssetContents.ImageName,
	@AssetImagePathWinFS = AssetContents.ImagePathWinFS,
	@Length = AssetContents.Length,
	@UserGivenDate = AssetContents.UserGivenDate
FROM
	AssetContents
WHERE
	AssetContents.ASSETCONTENT_ID = @AssetContentID;

SET @SlideFilenameNoPath = @GUID + @AssetFilenameExtension;
SET @SlideFilenameWithPath = @SubDir + '\' + @SlideFilenameNoPath;
SET @ImageName = @GUID + '.jpg';
	
IF (@AssetFilenameExtension IN ('.jpeg','.jpg','.jpe','.gif','.png','.tif','.tiff','.bmp'))
	SET @PlayerType = 'Image';

IF (@AssetFilenameExtension = '.swf')
	SET @PlayerType = 'Flash';
	
IF (@AssetFilenameExtension IN ('.avi','.wmv'))
	SET @PlayerType = 'VideoNonQT';
	
IF (@AssetFilenameExtension = '.mov')
	SET @PlayerType = 'VideoQT';

INSERT INTO Slides(SlideFolderID, 
					GUID, 
					SubDir, 
					SlideName,
					Creator, 
					Caption,
					ClickThroughURL, 
					DisplayDuration, 
					Length, 
					Filename, 
					FilenameExtension, 
					FilenameNoPath, 
					ImagePath,
					ImagePathWinFS,
					ImageFilename,
					PlayerType,
					PreviewType,
					bLocked,
					UserGivenDate,
					MadeDirtyLastDate)
		VALUES (@SlideFolderID, 
				@GUID, 
				@SubDir, 
				@SlideName, 
				@Creator,
				@Caption, 
				@URL,
				@Duration, 
				@Length, 
				@SlideFilenameWithPath, 
				@AssetFilenameExtension,
				@SlideFilenameNoPath, 
				@SubDir + '/' + @ImageName, 
				@SubDir + '\' + @ImageName, 
				@ImageName, 
				@PlayerType,
				@PreviewType, 
				0, -- when first created, slide will be unlocked --
				@UserGivenDate, 
				GETDATE());
GO
/****** Object:  StoredProcedure [dbo].[dp_editMoveSlideContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editMoveSlideContent]

(
@UserID int,
@OldFolderID int,
@NewFolderID int,
@SlideID int
)

AS

IF NOT EXISTS (SELECT 1 
				FROM
				Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
				INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID
					AND SlideFolders.SLIDEFOLDER_ID = @OldFolderID
					AND Slides.SLIDE_ID = @SlideID)
	OR NOT EXISTS (SELECT 1 
				FROM
				SlideFolders INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID
					AND SlideFolders.SLIDEFOLDER_ID = @NewFolderID)					
RETURN;

UPDATE
	Slides
SET
	SlideFolderID = @NewFolderID
WHERE
	SLIDE_ID = @SlideID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editMovePCChannel]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editMovePCChannel]

(
@UserID int,
@OldPCID int,
@NewPCID int,
@ChannelID int
)

AS

IF NOT EXISTS (SELECT 1 
				FROM
				PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				INNER JOIN PcChannels ON PCs.PC_ID = PcChannels.PcID
				WHERE
					Consumers.UserID = @UserID
					AND PCs.PC_ID = @OldPCID
					AND PcChannels.ChannelID = @ChannelID)
	OR NOT EXISTS (SELECT 1 
				FROM
				PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
				WHERE
					Consumers.UserID = @UserID
					AND PCs.PC_ID = @NewPCID)					
RETURN;

UPDATE
	PcChannels
SET
	PcID = @NewPCID
WHERE
	PcID = @OldPCID
	AND ChannelID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editMoveAssetContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editMoveAssetContent]

(
@UserID int,
@OldFolderID int,
@NewFolderID int,
@AssetContentID int
)

AS

IF NOT EXISTS (SELECT 1 
				FROM
				AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
				INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID
					AND AssetContentFolders.ASSETCONTENTFOLDER_ID = @OldFolderID
					AND AssetContents.ASSETCONTENT_ID = @AssetContentID)
	OR NOT EXISTS (SELECT 1 
				FROM
				AssetContentFolders INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID
					AND AssetContentFolders.ASSETCONTENTFOLDER_ID = @NewFolderID)				
RETURN;

UPDATE
	AssetContents
SET
	AssetContentFolderID = @NewFolderID
WHERE
	ASSETCONTENT_ID = @AssetContentID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editCompareMacAddressesLink]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editCompareMacAddressesLink]
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
		bLinkedToClient = 1,
		MajorVersionNumber = @SoftwareMajorVersionNumber,
		MinorVersionNumber = @SoftwareMinorVersionNumber
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
			
	INSERT INTO PCs(PCGUID, MajorVersionNumber, MinorVersionNumber, MACAddress, bLinkedToClient, ConsumerID, AddDate, PcName)
	VALUES (@NewMachineGUID, @SoftwareMajorVersionNumber, @SoftwareMinorVersionNumber, @MacAddressClient, 1, @ConsumerID, GETDATE(), 'New PC');
	
END
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelWeighting]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelWeighting]
(
@UserID int,
@PCID int,
@ChannelID int,
@ChannelWeighting int
)

AS

UPDATE
	PcChannels
SET
	ChannelWeighting = @ChannelWeighting
FROM
	PcChannels INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
	INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID
	AND PcChannels.PcID = @PCID
	AND PcChannels.ChannelID = @ChannelID

UPDATE
	PCs
SET
	MadeLastDirtyDate = GETDATE()
WHERE
	PC_ID = @PCID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelThumbnailIfNotExists]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelThumbnailIfNotExists]

(
@UserID int,
@ChannelID int,
@SlideID int,
@NewThumbnailGUID nvarchar(50),
@SlideThumbnailWithPartialPath nvarchar(255) OUTPUT,
@ChannelGUID nvarchar(40) OUTPUT
)

AS


-- does user have access --
IF NOT EXISTS (SELECT 1 FROM Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID WHERE Publishers.UserID = @UserID)
	RETURN;

-- does channel already have a thumbnail other than the default one --
IF EXISTS (SELECT 1 FROM Channels WHERE Channels.CHANNEL_ID = @ChannelID AND bHasDefaultThumbnail = 0)
	RETURN;
	
SELECT
	@SlideThumbnailWithPartialPath = Slides.ImagePath
FROM
	Slides
WHERE
	Slides.SLIDE_ID = @SlideID
	
SELECT
	@ChannelGUID = Channels.ChannelGUID
FROM
	Channels
WHERE
	Channels.CHANNEL_ID = @ChannelID;
	
UPDATE 
	Channels
SET
	ImagePath = SUBSTRING(ChannelGUID, LEN(ChannelGUID), 1) + '/' + @NewThumbnailGUID + '.jpg', -- all thumbnails uploaded from Aurigma are JPGs --
	bHasDefaultThumbnail = 0
WHERE
	CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editSlide]
(
@UserID int,
@SlideID int,
@Name nvarchar(100),
@Creator nvarchar(100) = null,
@Caption nvarchar(400) = null,
@UserGivenDate datetime = null,
@URL nvarchar(255) = null,
@DisplayDuration float
)

AS

UPDATE
	Slides
SET
	SlideName = @Name,
	Creator = @Creator,
	Caption = @Caption,
	UserGivenDate = @UserGivenDate,
	ClickThroughURL = @URL,
	DisplayDuration = @DisplayDuration
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND Slides.SLIDE_ID = @SlideID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentFoldersAll]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentFoldersAll]
(
@UserID int
)

AS

SELECT
	AssetContentFolders.ASSETCONTENTFOLDER_ID,
	AssetContents.ASSETCONTENT_ID,
	AssetContents.Name,
	AssetContents.ImagePath
FROM
	AssetContents RIGHT OUTER JOIN (AssetContentFolders INNER JOIN Publishers ON Publishers.PUBLISHER_ID = AssetContentFolders.PublisherID) 
	ON AssetContentFolders.ASSETCONTENTFOLDER_ID = AssetContents.AssetContentFolderID
	
WHERE
	Publishers.UserID = @UserID
ORDER BY 
	AssetContentFolders.ASSETCONTENTFOLDER_ID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentFolderPages]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentFolderPages]
(
@UserID int,
@AssetContentFolderID int
)

AS

SELECT
	CONVERT(int, CEILING(CONVERT(float, COUNT(AssetContents.ASSETCONTENT_ID)) / CONVERT(float, 12))) AS NoPages
FROM
	AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND AssetContentFolders.ASSETCONTENTFOLDER_ID = @AssetContentFolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentFilenamesToDelete]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentFilenamesToDelete]

(@UserID int)

AS

SELECT
	AssetContents.ImagePathWinFS,
	AssetContents.Filename,
	AssetContents.FilenameNoPath
FROM
	AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID  = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentByFolderID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentByFolderID]
(
@UserID int,
@AssetContentFolderID int
)

AS

SELECT
	ASSETCONTENT_ID,
	Name,
	ImagePath
FROM
	AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID 
WHERE
	Publishers.UserID = @UserID
	AND AssetContentFolders.ASSETCONTENTFOLDER_ID = @AssetContentFolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_editSlideMakeClean]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editSlideMakeClean]
(
@SlideID int
)

AS

UPDATE
	Slides
SET
	MadeDirtyLastDate = NULL
WHERE
	SLIDE_ID = @SlideID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getAssetContentPropertiesByContentID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getAssetContentPropertiesByContentID]
(
@UserID int,
@AssetContentID int
)

AS

-- check if consumer has access --
IF NOT EXISTS(SELECT 1 FROM 
				AssetContentFolders, 
				AssetContents,
				Publishers
				WHERE 
				AssetContentFolders.ASSETCONTENTFOLDER_ID = AssetContents.AssetContentFolderID 
				AND AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
				AND Publishers.UserID = @UserID
				AND AssetContents.ASSETCONTENT_ID = @AssetContentID)
				
	SET @AssetContentID = -1;

SELECT
	AssetContents.ASSETCONTENT_ID,
	AssetContents.Name,
	AssetContents.Creator,
	AssetContents.Caption,
	AssetContents.UserGivenDate,
	AssetContents.URL,
	AssetContents.DisplayDuration
FROM
	AssetContents
WHERE
	ASSETCONTENT_ID = @AssetContentID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelFull]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelFull]
(
@UserID int,
@ChannelID int
)
AS

SELECT
	CHANNEL_ID,
	ChannelGUID,
	ChannelName,
	ChannelDescription,
	ChannelLongDescription,
	NoContent,
	NoFollowers,
	Channels.AddDate,
	Channels.EditDate,
	DisplayName,
	ContentLastAddedDate,
	dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus
FROM
	Channels,
	Publishers
WHERE
	Channels.CHANNEL_ID = @ChannelID
	AND Publishers.PUBLISHER_ID = Channels.PublisherID
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByPCIDUnregistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByPCIDUnregistered]
(
@PcID int
)

AS
	
SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ImagePath,
	'Public' AS PrivacyStatus,
	PcChannels.ChannelWeighting
FROM
	Channels INNER JOIN PcChannels ON Channels.CHANNEL_ID = PcChannels.ChannelID
WHERE 
	PcChannels.PcID = @PcID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByPCID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByPCID]
(
@UserID int,
@PcID int
)

AS
	
SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ImagePath,
	dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
	PcChannels.ChannelWeighting,
	Channels.bAcceptPasswordRequests
FROM
	Channels,
	PcChannels,
	PCs,
	Consumers
WHERE
	Channels.CHANNEL_ID = PcChannels.ChannelID
	AND PCs.PC_ID = PcChannels.PcID
	AND PCs.ConsumerID = Consumers.CONSUMER_ID
	AND Consumers.UserID = @UserID
	AND PcChannels.PcID = @PcID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getDataForPreviewSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getDataForPreviewSlide]

(
@UserID int,
@ID int,
@SubDir char(1) OUTPUT,
@MediaGUID nvarchar(50) OUTPUT,
@FilenameWithSubdir nvarchar(100) OUTPUT,
@PreviewType nvarchar(10) OUTPUT
)

AS

SELECT
	@MediaGUID = Slides.GUID,
	@SubDir = Slides.SubDir,
	@FilenameWithSubdir = Slides.Filename,
	@PreviewType = Slides.PreviewType
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Slides.SLIDE_ID = @ID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeAssetContentFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeAssetContentFolder]
(
@UserID int,
@AssetContentFolderID int,
@FilesLength int OUTPUT
)

AS

DECLARE @PublisherID int;

SELECT
	@PublisherID = Publishers.PUBLISHER_ID
FROM
	Publishers
WHERE
	Publishers.UserID = @UserID;

IF NOT EXISTS (SELECT 1 FROM AssetContentFolders WHERE AssetContentFolders.PublisherID = @PublisherID)
RETURN;

IF EXISTS (SELECT 1 FROM AssetContents WHERE AssetContentFolderID = @AssetContentFolderID)
BEGIN

	SELECT 
		@FilesLength = SUM(AssetContents.Length)
	FROM
		AssetContents
	WHERE
		AssetContentFolderID = @AssetContentFolderID;

	DELETE FROM
		AssetContents
	WHERE
		AssetContentFolderID = @AssetContentFolderID;

	UPDATE 
		Publishers
	SET
		UsedBytes = UsedBytes - @FilesLength
	WHERE
		Publishers.UserID = @UserID;

END
ELSE
BEGIN

	SET @FilesLength = 0;

END
	
DELETE FROM 
	AssetContentFolders
WHERE
	ASSETCONTENTFOLDER_ID = @AssetContentFolderID;
	
-- if no more Asset Content Folders left for that account, create one and assign it a default name --
IF NOT EXISTS (SELECT 1 FROM AssetContentFolders WHERE AssetContentFolders.PublisherID = @PublisherID)
BEGIN

	INSERT INTO AssetContentFolders (PublisherID, AssetContentFolderName) VALUES (@PublisherID, 'My Content Folder');

END
GO
/****** Object:  StoredProcedure [dbo].[dp_removeAssetContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeAssetContent]
(
@UserID int,
@AssetContentFolderID int,
@AssetContentID int,
@ImagePathWinFS nvarchar(255) OUTPUT,
@Filename nvarchar(255) OUTPUT,
@FileLength int OUTPUT
)

AS

IF NOT EXISTS (SELECT 1 
				FROM 
				AssetContentFolders INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID 
				INNER JOIN AssetContents ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
				AND Publishers.UserID = @UserID
				AND AssetContents.ASSETCONTENT_ID = @AssetContentID)
BEGIN				
	SET @Filename = '';
	SET @ImagePathWinFS = '';
	
	RETURN;
END

SELECT 
	@Filename = AssetContents.Filename,
	@ImagePathWinFS = AssetContents.ImagePathWinFS,
	@FileLength = AssetContents.Length
FROM 
	AssetContents
WHERE
	ASSETCONTENT_ID = @AssetContentID;

DELETE FROM
	AssetContents
WHERE
	ASSETCONTENT_ID = @AssetContentID;
	
UPDATE 
	Publishers
SET
	UsedBytes = UsedBytes - @FileLength
WHERE
	Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeAllStreamsByMACAddress]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeAllStreamsByMACAddress]
(
@MACAddress nvarchar(50)
)

AS

DECLARE @PCID int;

SELECT
	@PCID = PCs.PC_ID
FROM
	PCs
WHERE
	PCs.MACAddress = @MACAddress;

DELETE FROM
	PcChannels
WHERE
	PcID = @PCID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getUserMachineChannelSubscriptions]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getUserMachineChannelSubscriptions]
(
@PCGUID nvarchar(50)
)

AS

DECLARE @UserID int;

SELECT
	@UserID = Consumers.UserID
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	PCs.PCGUID = @PCGUID;

SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelGUID,
	Channels.ChannelName,
	PcChannels.ChannelWeighting,
	CAST (CASE WHEN Channels.bLocked = 0 THEN 0 WHEN Channels.bLocked = 1 AND EXISTS (SELECT 1 FROM ChannelPasswordUnlock 
										WHERE ChannelPasswordUnlock.ChannelID = Channels.CHANNEL_ID
										AND ChannelPasswordUnlock.UserID = @UserID) THEN 0 ELSE 1 END AS BIT) AS bLocked
FROM
	Channels INNER JOIN PcChannels ON Channels.CHANNEL_ID = PcChannels.ChannelID
	INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
WHERE
	PCs.PCGUID = @PCGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getDataForPreviewAssetContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getDataForPreviewAssetContent]

(
@UserID int,
@ID int,
@SubDir char(1) OUTPUT,
@MediaGUID nvarchar(50) OUTPUT,
@FilenameWithSubdir nvarchar(100) OUTPUT,
@PreviewType nvarchar(10) OUTPUT
)

AS

SELECT
	@MediaGUID = AssetContents.GUID,
	@SubDir = AssetContents.SubDir,
	@FilenameWithSubdir = AssetContents.Filename,
	@PreviewType = AssetContents.PreviewType	
FROM
	AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	AssetContents.ASSETCONTENT_ID = @ID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getStreamSlidesBySlideFolderID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getStreamSlidesBySlideFolderID]
(
@UserID int,
@SlideFolderID int
)

AS

SELECT
	SLIDE_ID,
	SlideName,
	ImagePath,
	bLocked
FROM
	Slides,
	SlideFolders,
	Publishers
WHERE
	SlideFolders.SLIDEFOLDER_ID = @SlideFolderID
	AND SlideFolders.SLIDEFOLDER_ID = Slides.SlideFolderID
	AND SlideFolders.PublisherID = Publishers.PUBLISHER_ID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPcSubscriptionsNotRegistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPcSubscriptionsNotRegistered]
(
@PcProfileToken nvarchar(50)
)

AS

SELECT
	PCs.PC_ID,
	PCs.PcName,
	Channels.CHANNEL_ID,
	Channels.ChannelGUID,
	Channels.ChannelName,	
	PcChannels.ChannelWeighting
FROM
	PCs INNER JOIN PcChannels ON PCs.PC_ID = PcChannels.PcID
	INNER JOIN Channels ON Channels.CHANNEL_ID = PcChannels.ChannelID
WHERE
	PCs.PCGUID = @PcProfileToken;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPcSubscriptions]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPcSubscriptions]
(
@UserID int
)

AS

SELECT
	PCs.PC_ID,
	PCs.PcName,
	Channels.CHANNEL_ID,
	Channels.ChannelGUID,
	Channels.ChannelName,	
	PcChannels.ChannelWeighting
FROM
	PCs INNER JOIN PcChannels ON PCs.PC_ID = PcChannels.PcID
	INNER JOIN Channels ON Channels.CHANNEL_ID = PcChannels.ChannelID
	INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPcStreamsAll]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPcStreamsAll]
(
@UserID int
)

AS

SELECT
	PCs.PC_ID,
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ImagePath
FROM
	PCs LEFT OUTER JOIN (PcChannels INNER JOIN Channels ON PcChannels.ChannelID = Channels.CHANNEL_ID)
		ON PCs.PC_ID = PcChannels.PcID
	INNER JOIN Consumers ON Consumers.CONSUMER_ID = PCs.ConsumerID
WHERE
	Consumers.UserID = @UserID
ORDER BY PCs.PC_ID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getSlidesDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSlidesDirty]

AS

SELECT
	Slides.SLIDE_ID,
	Slides.Filename,
	Slides.FilenameNoPath
FROM
	Slides
WHERE
	Slides.MadeDirtyLastDate IS NOT NULL
ORDER BY
	Slides.MadeDirtyLastDate;
GO
/****** Object:  StoredProcedure [dbo].[dp_getSlideFoldersAll]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSlideFoldersAll]
(
@UserID int
)

AS

SELECT
	SlideFolders.SLIDEFOLDER_ID,
	Slides.SLIDE_ID,
	Slides.SlideName,
	Slides.ImagePath
FROM
	Slides RIGHT OUTER JOIN (SlideFolders INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID)
	 ON SlideFolders.SLIDEFOLDER_ID = Slides.SlideFolderID	
WHERE
	Publishers.UserID = @UserID
ORDER BY
	SlideFolders.SLIDEFOLDER_ID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getSlideFolderPages]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSlideFolderPages]
(
@UserID int,
@SlideFolderID int
)

AS

SELECT
	CONVERT(int, CEILING(CONVERT(float, COUNT(Slides.SLIDE_ID)) / CONVERT(float, 12))) AS NoPages
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND SlideFolders.SLIDEFOLDER_ID = @SlideFolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getSlideFilenamesToDelete]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSlideFilenamesToDelete]

(@UserID int)

AS

SELECT
	Slides.ImagePathWinFS,
	Slides.Filename,
	Slides.FilenameNoPath
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID  = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getSlide]
(
@UserID int,
@SlideID int
)

AS

SELECT
	Slides.SlideName,
	Slides.Creator,
	Slides.Caption,
	Slides.DisplayDuration,
	Slides.UserGivenDate,
	Slides.ClickThroughURL
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND Slides.SLIDE_ID = @SlideID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getRemovableSlideContentsByAssetContentFolderID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getRemovableSlideContentsByAssetContentFolderID]
(
@UserID int,
@SlideFolderID int
)

AS

SELECT
	Slides.Filename,
	Slides.ImagePathWinFS
FROM
	Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
	INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	SlideFolders.SLIDEFOLDER_ID = @SlideFolderID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getRemovableAssetContentsByAssetContentFolderID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getRemovableAssetContentsByAssetContentFolderID]
(
@UserID int,
@AssetContentFolderID int
)

AS

SELECT
	AssetContents.Filename,
	AssetContents.ImagePathWinFS
FROM
	AssetContents INNER JOIN AssetContentFolders ON AssetContents.AssetContentFolderID = AssetContentFolders.ASSETCONTENTFOLDER_ID
	INNER JOIN Publishers ON AssetContentFolders.PublisherID = Publishers.PUBLISHER_ID
WHERE
	AssetContentFolders.ASSETCONTENTFOLDER_ID = @AssetContentFolderID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCChannelsByUserGUIDPCID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCChannelsByUserGUIDPCID]

(
@UserGUID nvarchar(50),
@PcID int
)

AS

SELECT
	Channels.ChannelGUID,
	Channels.ChannelName
FROM
	PcChannels INNER JOIN Channels ON PcChannels.ChannelID = Channels.CHANNEL_ID
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PcID = @PcID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPCChannelsByUserGUIDMachineGUID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPCChannelsByUserGUIDMachineGUID]

(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

SELECT
	Channels.ChannelGUID,
	Channels.ChannelName
FROM
	PcChannels INNER JOIN Channels ON PcChannels.ChannelID = Channels.CHANNEL_ID
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PCGUID = @MachineGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getPcChannelPages]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getPcChannelPages]
(
@PcID int
)

AS
	
SELECT
	CONVERT(int, CEILING(CONVERT(float, COUNT(Channels.CHANNEL_ID)) / CONVERT(float, 12))) AS NoPages
FROM
	Channels,
	PcChannels
WHERE
	Channels.CHANNEL_ID = PcChannels.ChannelID
	AND PcChannels.PcID = @PcID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removePhantomProfiles]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePhantomProfiles]

AS

DECLARE @dt DateTime;

SET @dt = GETDATE();

DELETE 
	PcChannels
FROM
	PcChannels INNER JOIN PCs ON PcChannels.PcID = PCs.PC_ID
WHERE
	PCs.bLinkedToClient = 0
	AND DATEDIFF(hour, PCs.AddDate, @dt) >= 2;
	
DELETE FROM
	PCs
WHERE
	PCs.bLinkedToClient = 0
	AND DATEDIFF(hour, PCs.AddDate, @dt) >= 2;
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCSubscriptionsLinkDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCSubscriptionsLinkDirty]
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
	PCs.MadeLastDirtyDate = GETDATE(),
	Pcs.MajorVersionNumber = @SoftwareMajorVersionNumber,
	pcs.MinorVersionNumber = @SoftwareMinorVersionNumber
WHERE
	PCs.PC_ID = @PCID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCSubscriptionsDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCSubscriptionsDirty]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

-- remove counter. no need to check if PC is "real". PC is always real as this procedure is called fro a real PC.
UPDATE
	Channels
SET
	NoFollowers = NoFollowers - 1
FROM
	Channels INNER JOIN PcChannels ON Channels.CHANNEL_ID = PcChannels.ChannelID
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PCGUID = @MachineGUID

DELETE 
	PCChannels
FROM
	PCChannels
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PCGUID = @MachineGUID
	
UPDATE
	PCs
SET
	PCs.MadeLastDirtyDate = GETDATE()
WHERE
	PCs.PCGUID = @MachineGUID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCSubscriptions]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCSubscriptions]
(
@UserGUID nvarchar(50),
@MachineGUID nvarchar(50)
)

AS

-- remove counter. no need to check if PC is "real". PC is always real as this procedure is called fro a real PC.
UPDATE
	Channels
SET
	NoFollowers = NoFollowers - 1
FROM
	Channels INNER JOIN PcChannels ON Channels.CHANNEL_ID = PcChannels.ChannelID
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PCGUID = @MachineGUID

DELETE 
	PCChannels
FROM
	PCChannels
WHERE
	PcChannels.UserGUID = @UserGUID
	AND PcChannels.PCGUID = @MachineGUID
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCStreamUnregistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCStreamUnregistered]
(
@PCID int,
@ChannelID int
)

AS 

-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM Pcs WHERE ConsumerID IS NULL AND PC_ID = @PCID)
RETURN;

DELETE FROM PcChannels
WHERE PcID = @PCID AND ChannelID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCStreamRegistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCStreamRegistered]
(
@UserID int,
@PCID int,
@ChannelID int
)

AS 

-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM Pcs, Consumers WHERE ConsumerID = CONSUMER_ID AND UserID = @UserID)
RETURN;

DELETE FROM PcChannels
WHERE PcID = @PCID AND ChannelID = @ChannelID;

IF EXISTS (SELECT 1 FROM PCs WHERE PCs.PC_ID = @PCID AND bLinkedToClient = 1)
	BEGIN

		UPDATE
			Channels
		SET
			NoFollowers = NoFollowers - 1
		WHERE
			Channels.CHANNEL_ID = @ChannelID;
			
			
		UPDATE
			PCs
		SET
			MadeLastDirtyDate = GETDATE()
		WHERE
			PC_ID = @PCID;
	
	END
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCStream]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCStream]
(
@UserID int,
@PCID int,
@ChannelID int
)

AS 

-- check if user has access to PC --
IF NOT EXISTS(SELECT 1 FROM Pcs, Consumers WHERE ConsumerID = CONSUMER_ID AND UserID = @UserID)
RETURN;

DELETE FROM PcChannels
WHERE PcID = @PCID AND ChannelID = @ChannelID;

IF EXISTS (SELECT 1 FROM PCs WHERE PCs.PC_ID = @PCID AND bLinkedToClient = 1)
	BEGIN

		UPDATE
			Channels
		SET
			NoFollowers = NoFollowers - 1
		WHERE
			Channels.CHANNEL_ID = @ChannelID;
			
			
		UPDATE
			PCs
		SET
			MadeLastDirtyDate = GETDATE()
		WHERE
			PC_ID = @PCID;
	
	END
GO
/****** Object:  StoredProcedure [dbo].[dp_removePCFromUninstall]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removePCFromUninstall]

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

	DELETE FROM PCs WHERE PCs.PC_ID = @PcID;

END
GO
/****** Object:  StoredProcedure [dbo].[dp_removeTemporaryPcProfilesNotRegistered]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeTemporaryPcProfilesNotRegistered]

(@PcProfileToken nvarchar(50))

AS

DELETE 
	PcChannels
FROM
	PCs INNER JOIN PcChannels ON PCs.PC_ID = PcChannels.PcID	
WHERE
	PCs.PCGUID = @PcProfileToken;

DELETE FROM
	PCs
WHERE
	PCs.PCGUID = @PcProfileToken;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeTemporaryPcProfiles]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeTemporaryPcProfiles]

(@UserID int)

AS

DELETE 
	PcChannels
FROM
	PCs INNER JOIN PcChannels ON PCs.PC_ID = PcChannels.PcID
	INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID
	AND PCs.bLinkedToClient = 0;
	
DELETE
	PCs
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID
	AND PCs.bLinkedToClient = 0;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeStreamByMacAddress]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeStreamByMacAddress]
(
@MACAddress nvarchar(50),
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
	PCs.MACAddress = @MACAddress;

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
/****** Object:  StoredProcedure [dbo].[dp_removeSlideFolder]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeSlideFolder]
(
@UserID int,
@SlideFolderID int
)

AS

DECLARE @PublisherID int;

SELECT
	@PublisherID = Publishers.PUBLISHER_ID
FROM
	Publishers
WHERE
	Publishers.UserID = @UserID;

IF NOT EXISTS (SELECT 1 FROM SlideFolders WHERE SlideFolders.PublisherID = @PublisherID)
RETURN;

DELETE ChannelsSlides
	FROM
	ChannelsSlides INNER JOIN Slides ON ChannelsSlides.SlideID = Slides.SLIDE_ID
WHERE
	Slides.SlideFolderID = @SlideFolderID;	

DELETE FROM
	Slides
WHERE
	SlideFolderID = @SlideFolderID;

DELETE FROM 
	SlideFolders
WHERE
	SLIDEFOLDER_ID = @SlideFolderID;
	
-- if no more Slide Folders left for that account, create one and assign it a default name --
IF NOT EXISTS (SELECT 1 FROM SlideFolders WHERE SlideFolders.PublisherID = @PublisherID)
BEGIN

	INSERT INTO SlideFolders (PublisherID, SlideFolderName) VALUES (@PublisherID, 'My Slide Folder');

END
GO
/****** Object:  StoredProcedure [dbo].[dp_removeSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeSlide]

(
@UserID int,
@SlideFolderID int,
@SlideID int,
@Filename nvarchar(255) OUTPUT,
@ImagePath nvarchar(255) OUTPUT
)

AS

IF NOT EXISTS (SELECT 1 FROM 
				Slides INNER JOIN SlideFolders ON Slides.SlideFolderID = SlideFolders.SLIDEFOLDER_ID
				INNER JOIN Publishers ON SlideFolders.PublisherID = Publishers.PUBLISHER_ID
				WHERE
				Slides.SLIDE_ID = @SlideID
				AND Publishers.UserID = @UserID)
				
	OR EXISTS (SELECT 1 FROM
				Slides INNER JOIN ChannelsSlides ON Slides.SLIDE_ID = ChannelsSlides.SlideID
				WHERE Slides.SLIDE_ID = @SlideID)
RETURN 0;

SELECT
	@Filename = Slides.Filename,
	@ImagePath = Slides.ImagePath
FROM
	Slides
WHERE
	Slides.SLIDE_ID = @SlideID;
	
DELETE FROM 
	Slides 
WHERE 
	Slides.SLIDE_ID = @SlideID;
	
RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeChannelContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeChannelContent]

(
@UserID int,
@ChannelID int,
@ChannelSlideID int
)

AS

IF NOT EXISTS (SELECT 1 
				FROM 
					Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID
					AND Channels.CHANNEL_ID = @ChannelID)
RETURN;

DELETE FROM
	ChannelsSlides
WHERE
	ChannelsSlides.ChannelID = @ChannelID
	AND ChannelsSlides.CHANNELSSLIDE_ID = @ChannelSlideID;
	
UPDATE
	Channels
SET
	MadeDirtyLastDate = GETDATE(),
	NoContent = NoContent - 1
WHERE
	CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_removeChannel]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeChannel]
(
@UserID int,
@ChannelID int,
@ChannelThumbnailWithPartialPath nvarchar(255) OUTPUT
)

AS

DECLARE @PublisherID int;
DECLARE @UserGUIDSuffix int;
DECLARE @ChannelGUID nvarchar(50);
DECLARE @ChannelGUIDSuffix char(1);

SELECT
	@PublisherID = Publishers.PUBLISHER_ID
FROM
	Publishers
WHERE
	Publishers.UserID = @UserID;
	
SELECT
	@ChannelThumbnailWithPartialPath = Channels.ImagePath
FROM
	Channels
WHERE
	Channels.CHANNEL_ID = @ChannelID;

IF NOT EXISTS (SELECT 1
				FROM
					Channels 
				WHERE
					Channels.PublisherID = @PublisherID)
	RETURN -1;
	
DELETE FROM ChannelsSlides WHERE ChannelID = @ChannelID;

DELETE FROM PcChannels WHERE ChannelID = @ChannelID;

DELETE FROM Channels WHERE CHANNEL_ID = @ChannelID;

-- if no more Channels left for that account, create one and assign it a default name --
IF NOT EXISTS (SELECT 1 FROM Channels WHERE Channels.PublisherID = @PublisherID)
BEGIN

	-- get user GUID suffix to use as the new channel's GUID suffix --
	SELECT 
		@ChannelGUIDSuffix = (substring(Users.UserGUID,len(Users.UserGUID),(1)))
	FROM
		Users
	WHERE
		Users.USER_ID = @UserID;
		
	SET @ChannelGUID = CONVERT(nvarchar(38), NEWID()) + '_' + SUBSTRING(@ChannelGUIDSuffix, 1, 1);

	INSERT INTO Channels(PublisherID, ChannelName, ChannelGUID, bHasDefaultThumbnail, bLocked, bAcceptPasswordRequests, NoContent, NoFollowers)
		VALUES(@PublisherID, 'My Stream', @ChannelGUID, 1, 0, 1, 0, 0);

END
GO
/****** Object:  StoredProcedure [dbo].[dp_removeUserAccount]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_removeUserAccount]

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
	PCs
FROM 
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	Consumers.UserID = @UserID;
	
DELETE FROM Consumers WHERE Consumers.UserID = @UserID;

DELETE FROM Publishers WHERE Publishers.UserID = @UserID;

DELETE FROM Users WHERE Users.USER_ID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getMostPopularChannels]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getMostPopularChannels]

AS

SELECT TOP 40
	Channel_ID,
	ChannelName
FROM
	Channels
WHERE
	EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channel_ID)
ORDER BY NoFollowers DESC;
GO
/****** Object:  StoredProcedure [dbo].[dp_getHasSlideFolderScheduledSlides]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getHasSlideFolderScheduledSlides]
(
@SlideFolderID int
)

AS

SELECT TOP 1 1
FROM
SlideFolders INNER JOIN Slides ON SlideFolders.SLIDEFOLDER_ID = Slides.SlideFolderID
INNER JOIN ChannelsSlides ON ChannelsSlides.SlideID = Slides.SLIDE_ID
WHERE
SlideFolders.SLIDEFOLDER_ID = @SlideFolderID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getDataForPreviewChannelSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getDataForPreviewChannelSlide]

(
@UserID int,
@ID int,
@SubDir char(1) OUTPUT,
@MediaGUID nvarchar(50) OUTPUT,
@FilenameWithSubdir nvarchar(100) OUTPUT,
@PreviewType nvarchar(10) OUTPUT
)

AS

SELECT
	@MediaGUID = Slides.GUID,
	@SubDir = Slides.SubDir,
	@FilenameWithSubdir = Slides.Filename,
	@PreviewType = Slides.PreviewType
FROM
	Slides INNER JOIN ChannelsSlides ON Slides.SLIDE_ID = ChannelsSlides.SlideID
	INNER JOIN Channels ON ChannelsSlides.ChannelID = Channels.CHANNEL_ID
WHERE
	ChannelsSlides.CHANNELSSLIDE_ID = @ID
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelsDirty]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelsDirty]

AS

SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelGUID,
	CONVERT(decimal(2,2), 0.5) AS VotingThreshold, -- TODO: make dynamic
	Categories.CTT,
	Slides.SLIDE_ID,
	Slides.PlayerType,
	Slides.FilenameNopath,
	'Normal' AS AssetLevel, -- TODO: Normal or Premium
	ChannelsSlides.DisplayDuration,
	Slides.WebsiteURL,
	ChannelsSlides.ClickThroughURL,
	ChannelsSlides.Schedule,
	CONVERT(datetime, '2010-01-01') AS StartDateTime,
	CONVERT(datetime, '2012-01-01') AS EndDateTime
FROM
	Channels LEFT OUTER JOIN (ChannelsSlides INNER JOIN Slides ON ChannelsSlides.SlideID = Slides.SLIDE_ID)
	ON Channels.CHANNEL_ID = ChannelsSlides.ChannelID
	LEFT OUTER JOIN Categories ON Categories.CATEGORY_ID = Channels.CategoryID
WHERE
	Channels.MadeDirtyLastDate IS NOT NULL
ORDER BY
	Channels.MadeDirtyLastDate;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelsByUserID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelsByUserID]

(@UserID int)

AS

SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelGUID,
	Channels.ChannelName
FROm
	Channels INNER JOIN Publishers ON Publishers.PUBLISHER_ID = Channels.PublisherID
WHERE
	Publishers.UserID = @UserID
	AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE Channels.CHANNEL_ID = ChannelsSlides.ChannelID)
ORDER BY
	Channels.CHANNEL_ID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelSlidesByChannelID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelSlidesByChannelID]
(
@UserID int,
@ChannelID int
)

AS

SELECT
	ChannelsSlides.CHANNELSSLIDE_ID,
	Slides.SlideName,
	Slides.ImagePath,
	Slides.bLocked
FROM
	Channels INNER JOIN ChannelsSlides ON Channels.CHANNEL_ID = ChannelsSlides.ChannelID
	INNER JOIN Slides ON ChannelsSlides.SlideID = Slides.SLIDE_ID
	INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND Channels.CHANNEL_ID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelSlides]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelSlides]
(
@UserID int,
@ChannelID int
)
AS

SELECT
	ChannelsSlides.CHANNELSSLIDE_ID,
	Slides.SlideName,
	Slides.ImagePath
FROM
	ChannelsSlides INNER JOIN Slides ON ChannelsSlides.SlideID = Slides.SLIDE_ID
WHERE
	ChannelsSlides.ChannelID = @ChannelID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelSlideProperties]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelSlideProperties]

(
@UserID int,
@ChannelsSlideID int
)

AS

SELECT
	ChannelsSlides.ClickThroughURL,
	ChannelsSlides.DisplayDuration,
	ChannelsSlides.PresentationConvertedSchedule
FROM
	ChannelsSlides INNER JOIN Channels ON ChannelsSlides.ChannelID = Channels.CHANNEL_ID
	INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	ChannelsSlides.CHANNELSSLIDE_ID = @ChannelsSlideID
	AND Publishers.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelSlidePages]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelSlidePages]
(
@UserID int,
@ChannelID int
)

AS

SELECT
	Channels.ImagePath,
	CONVERT(int, CEILING(CONVERT(float, COUNT(Slides.SLIDE_ID)) / CONVERT(float, 12))) AS NoPages
FROM
	Slides INNER JOIN ChannelsSlides ON Slides.SLIDE_ID = ChannelsSlides.SlideID
	RIGHT OUTER JOIN Channels ON ChannelsSlides.ChannelID = Channels.CHANNEL_ID
	INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	Publishers.UserID = @UserID
	AND Channels.CHANNEL_ID = @ChannelID
GROUP BY
	Channels.ImagePath;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByText]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByText]
(
@UserID int,
@Text nvarchar(1000)
)

AS

SET @Text = '"' + REPLACE(@Text, '"', '') + '*"';

SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ImagePath,
	dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
	Channels.bAcceptPasswordRequests
FROM
	Channels INNER JOIN CONTAINSTABLE(Channels, *, @Text) AS Name
	ON Channels.CHANNEL_ID = Name.[KEY]
WHERE EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID);
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelFullPopularTop5ByCategoryID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelFullPopularTop5ByCategoryID]
(
@CategoryID int
)
AS

DECLARE @CTT nvarchar(4000);
DECLARE @TempT TABLE (OrderNo int, CTTNode int);

DECLARE @CTTLevel1 int;
DECLARE @CTTLevel2 int;
DECLARE @CTTLevel3 int;
DECLARE @CTTLevel4 int;
DECLARE @CTTLevel5 int;
DECLARE @CTTLevel6 int;
DECLARE @CTTLevel7 int;
DECLARE @CTTLevel8 int;
DECLARE @CTTLevel9 int;
DECLARE @CTTLevel10 int;

SELECT 
	@CTT = Categories.CTT 
FROM 
	Categories 
WHERE 
	Categories.CATEGORY_ID = @CategoryID

-- split the CTT into a temp table --
INSERT INTO @TempT (OrderNo, CTTNode)
SELECT IDENTITY_ID, Data
FROM fn_SplitCDLToTableInt(@CTT, '.');

SELECT @CTTLevel1 = CTTNode FROM @TempT WHERE OrderNo = 1;
SELECT @CTTLevel2 = CTTNode FROM @TempT WHERE OrderNo = 2;
SELECT @CTTLevel3 = CTTNode FROM @TempT WHERE OrderNo = 3;
SELECT @CTTLevel4 = CTTNode FROM @TempT WHERE OrderNo = 4;
SELECT @CTTLevel5 = CTTNode FROM @TempT WHERE OrderNo = 5;
SELECT @CTTLevel6 = CTTNode FROM @TempT WHERE OrderNo = 6;
SELECT @CTTLevel7 = CTTNode FROM @TempT WHERE OrderNo = 7;
SELECT @CTTLevel8 = CTTNode FROM @TempT WHERE OrderNo = 8;
SELECT @CTTLevel9 = CTTNode FROM @TempT WHERE OrderNo = 9;
SELECT @CTTLevel10 = CTTNode FROM @TempT WHERE OrderNo = 10;

SELECT TOP 5
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ChannelDescription,
	Channels.ChannelLongDescription,
	Channels.NoContent,
	Channels.NoFollowers,
	Channels.ImagePath,
	Channels.AddDate,
	Publishers.DisplayName
FROM
	Channels INNER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
	INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
WHERE
	(@CTTLevel1 IS NULL OR Categories.CTTLevel1 = @CTTLevel1)
	AND (@CTTLevel2 IS NULL OR Categories.CTTLevel2 = @CTTLevel2)
	AND (@CTTLevel3 IS NULL OR Categories.CTTLevel3 = @CTTLevel3)
	AND (@CTTLevel4 IS NULL OR Categories.CTTLevel4 = @CTTLevel4)
	AND (@CTTLevel5 IS NULL OR Categories.CTTLevel5 = @CTTLevel5)
	AND (@CTTLevel6 IS NULL OR Categories.CTTLevel6 = @CTTLevel6)
	AND (@CTTLevel7 IS NULL OR Categories.CTTLevel7 = @CTTLevel7)
	AND (@CTTLevel8 IS NULL OR Categories.CTTLevel8 = @CTTLevel8)
	AND (@CTTLevel9 IS NULL OR Categories.CTTLevel9 = @CTTLevel9)
	AND (@CTTLevel10 IS NULL OR Categories.CTTLevel10 = @CTTLevel10)
	AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID)
ORDER BY
	Channels.NoFollowers DESC;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByCategoryIDPopularity]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByCategoryIDPopularity]
(
@UserID int,
@CategoryID int,
@StartChannelNo int,
@EndChannelNo int
)

AS

DECLARE @CTT nvarchar(4000);
DECLARE @TempT TABLE (OrderNo int, CTTNode int);

DECLARE @CTTLevel1 int;
DECLARE @CTTLevel2 int;
DECLARE @CTTLevel3 int;
DECLARE @CTTLevel4 int;
DECLARE @CTTLevel5 int;
DECLARE @CTTLevel6 int;
DECLARE @CTTLevel7 int;
DECLARE @CTTLevel8 int;
DECLARE @CTTLevel9 int;
DECLARE @CTTLevel10 int;

SELECT 
	@CTT = Categories.CTT 
FROM 
	Categories 
WHERE 
	Categories.CATEGORY_ID = @CategoryID

-- split the CTT into a temp table --
INSERT INTO @TempT (OrderNo, CTTNode)
SELECT IDENTITY_ID, Data
FROM fn_SplitCDLToTableInt(@CTT, '.');

SELECT @CTTLevel1 = CTTNode FROM @TempT WHERE OrderNo = 1;
SELECT @CTTLevel2 = CTTNode FROM @TempT WHERE OrderNo = 2;
SELECT @CTTLevel3 = CTTNode FROM @TempT WHERE OrderNo = 3;
SELECT @CTTLevel4 = CTTNode FROM @TempT WHERE OrderNo = 4;
SELECT @CTTLevel5 = CTTNode FROM @TempT WHERE OrderNo = 5;
SELECT @CTTLevel6 = CTTNode FROM @TempT WHERE OrderNo = 6;
SELECT @CTTLevel7 = CTTNode FROM @TempT WHERE OrderNo = 7;
SELECT @CTTLevel8 = CTTNode FROM @TempT WHERE OrderNo = 8;
SELECT @CTTLevel9 = CTTNode FROM @TempT WHERE OrderNo = 9;
SELECT @CTTLevel10 = CTTNode FROM @TempT WHERE OrderNo = 10;

SELECT
	ChannelsWithRowNumbers.CHANNEL_ID,
	ChannelsWithRowNumbers.ChannelName,
	ChannelsWithRowNumbers.ImagePath,
	ChannelsWithRowNumbers.PrivacyStatus,
	ChannelsWithRowNumbers.bAcceptPasswordRequests
FROM
	(SELECT ROW_NUMBER() OVER (ORDER BY Channels.NoFollowers DESC) AS Row,
		Channels.CHANNEL_ID, 
		Channels.ChannelName, 
		Channels.ImagePath, 
		dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
		Channels.bAcceptPasswordRequests
	FROM 
		Channels LEFT OUTER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
	WHERE
		(@CTTLevel1 IS NULL OR Categories.CTTLevel1 = @CTTLevel1)
		AND (@CTTLevel2 IS NULL OR Categories.CTTLevel2 = @CTTLevel2)
		AND (@CTTLevel3 IS NULL OR Categories.CTTLevel3 = @CTTLevel3)
		AND (@CTTLevel4 IS NULL OR Categories.CTTLevel4 = @CTTLevel4)
		AND (@CTTLevel5 IS NULL OR Categories.CTTLevel5 = @CTTLevel5)
		AND (@CTTLevel6 IS NULL OR Categories.CTTLevel6 = @CTTLevel6)
		AND (@CTTLevel7 IS NULL OR Categories.CTTLevel7 = @CTTLevel7)
		AND (@CTTLevel8 IS NULL OR Categories.CTTLevel8 = @CTTLevel8)
		AND (@CTTLevel9 IS NULL OR Categories.CTTLevel9 = @CTTLevel9)
		AND (@CTTLevel10 IS NULL OR Categories.CTTLevel10 = @CTTLevel10)
		AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID))AS ChannelsWithRowNumbers
WHERE 
	ChannelsWithRowNumbers.Row >= @StartChannelNo 
	AND ChannelsWithRowNumbers.Row <= @EndChannelNo;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByCategoryIDMostRecent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByCategoryIDMostRecent]
(
@UserID int,
@CategoryID int,
@StartChannelNo int,
@EndChannelNo int
)

AS

DECLARE @CTT nvarchar(4000);
DECLARE @TempT TABLE (OrderNo int, CTTNode int);

DECLARE @CTTLevel1 int;
DECLARE @CTTLevel2 int;
DECLARE @CTTLevel3 int;
DECLARE @CTTLevel4 int;
DECLARE @CTTLevel5 int;
DECLARE @CTTLevel6 int;
DECLARE @CTTLevel7 int;
DECLARE @CTTLevel8 int;
DECLARE @CTTLevel9 int;
DECLARE @CTTLevel10 int;

SELECT 
	@CTT = Categories.CTT 
FROM 
	Categories 
WHERE 
	Categories.CATEGORY_ID = @CategoryID

-- split the CTT into a temp table --
INSERT INTO @TempT (OrderNo, CTTNode)
SELECT IDENTITY_ID, Data
FROM fn_SplitCDLToTableInt(@CTT, '.');

SELECT @CTTLevel1 = CTTNode FROM @TempT WHERE OrderNo = 1;
SELECT @CTTLevel2 = CTTNode FROM @TempT WHERE OrderNo = 2;
SELECT @CTTLevel3 = CTTNode FROM @TempT WHERE OrderNo = 3;
SELECT @CTTLevel4 = CTTNode FROM @TempT WHERE OrderNo = 4;
SELECT @CTTLevel5 = CTTNode FROM @TempT WHERE OrderNo = 5;
SELECT @CTTLevel6 = CTTNode FROM @TempT WHERE OrderNo = 6;
SELECT @CTTLevel7 = CTTNode FROM @TempT WHERE OrderNo = 7;
SELECT @CTTLevel8 = CTTNode FROM @TempT WHERE OrderNo = 8;
SELECT @CTTLevel9 = CTTNode FROM @TempT WHERE OrderNo = 9;
SELECT @CTTLevel10 = CTTNode FROM @TempT WHERE OrderNo = 10;

SELECT
	ChannelsWithRowNumbers.CHANNEL_ID,
	ChannelsWithRowNumbers.ChannelName,
	ChannelsWithRowNumbers.ImagePath,
	ChannelsWithRowNumbers.PrivacyStatus,
	ChannelsWithRowNumbers.bAcceptPasswordRequests
FROM
	(SELECT ROW_NUMBER() OVER (ORDER BY Channels.AddDate DESC) AS Row,
		Channels.CHANNEL_ID, 
		Channels.ChannelName, 
		Channels.ImagePath, 
		dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
		Channels.bAcceptPasswordRequests
	FROM 
		Channels LEFT OUTER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
	WHERE
		(@CTTLevel1 IS NULL OR Categories.CTTLevel1 = @CTTLevel1)
		AND (@CTTLevel2 IS NULL OR Categories.CTTLevel2 = @CTTLevel2)
		AND (@CTTLevel3 IS NULL OR Categories.CTTLevel3 = @CTTLevel3)
		AND (@CTTLevel4 IS NULL OR Categories.CTTLevel4 = @CTTLevel4)
		AND (@CTTLevel5 IS NULL OR Categories.CTTLevel5 = @CTTLevel5)
		AND (@CTTLevel6 IS NULL OR Categories.CTTLevel6 = @CTTLevel6)
		AND (@CTTLevel7 IS NULL OR Categories.CTTLevel7 = @CTTLevel7)
		AND (@CTTLevel8 IS NULL OR Categories.CTTLevel8 = @CTTLevel8)
		AND (@CTTLevel9 IS NULL OR Categories.CTTLevel9 = @CTTLevel9)
		AND (@CTTLevel10 IS NULL OR Categories.CTTLevel10 = @CTTLevel10)
		AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID))AS ChannelsWithRowNumbers
WHERE 
	ChannelsWithRowNumbers.Row >= @StartChannelNo 
	AND ChannelsWithRowNumbers.Row <= @EndChannelNo;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelListByCategoryIDAlpha]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelListByCategoryIDAlpha]
(
@UserID int,
@CategoryID int,
@StartChannelNo int,
@EndChannelNo int
)

AS

DECLARE @CTT nvarchar(4000);
DECLARE @TempT TABLE (OrderNo int, CTTNode int);

DECLARE @CTTLevel1 int;
DECLARE @CTTLevel2 int;
DECLARE @CTTLevel3 int;
DECLARE @CTTLevel4 int;
DECLARE @CTTLevel5 int;
DECLARE @CTTLevel6 int;
DECLARE @CTTLevel7 int;
DECLARE @CTTLevel8 int;
DECLARE @CTTLevel9 int;
DECLARE @CTTLevel10 int;

SELECT 
	@CTT = Categories.CTT 
FROM 
	Categories 
WHERE 
	Categories.CATEGORY_ID = @CategoryID

-- split the CTT into a temp table --
INSERT INTO @TempT (OrderNo, CTTNode)
SELECT IDENTITY_ID, Data
FROM fn_SplitCDLToTableInt(@CTT, '.');

SELECT @CTTLevel1 = CTTNode FROM @TempT WHERE OrderNo = 1;
SELECT @CTTLevel2 = CTTNode FROM @TempT WHERE OrderNo = 2;
SELECT @CTTLevel3 = CTTNode FROM @TempT WHERE OrderNo = 3;
SELECT @CTTLevel4 = CTTNode FROM @TempT WHERE OrderNo = 4;
SELECT @CTTLevel5 = CTTNode FROM @TempT WHERE OrderNo = 5;
SELECT @CTTLevel6 = CTTNode FROM @TempT WHERE OrderNo = 6;
SELECT @CTTLevel7 = CTTNode FROM @TempT WHERE OrderNo = 7;
SELECT @CTTLevel8 = CTTNode FROM @TempT WHERE OrderNo = 8;
SELECT @CTTLevel9 = CTTNode FROM @TempT WHERE OrderNo = 9;
SELECT @CTTLevel10 = CTTNode FROM @TempT WHERE OrderNo = 10;

SELECT
	ChannelsWithRowNumbers.CHANNEL_ID,
	ChannelsWithRowNumbers.ChannelName,
	ChannelsWithRowNumbers.ImagePath,
	ChannelsWithRowNumbers.PrivacyStatus,
	ChannelsWithRowNumbers.bAcceptPasswordRequests
FROM
	(SELECT ROW_NUMBER() OVER (ORDER BY Channels.ChannelName) AS Row,
		Channels.CHANNEL_ID, 
		Channels.ChannelName, 
		Channels.ImagePath, 
		dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
		Channels.bAcceptPasswordRequests
	FROM 
		Channels LEFT OUTER JOIN Categories ON Channels.CategoryID = Categories.CATEGORY_ID
	WHERE
		(@CTTLevel1 IS NULL OR Categories.CTTLevel1 = @CTTLevel1)
		AND (@CTTLevel2 IS NULL OR Categories.CTTLevel2 = @CTTLevel2)
		AND (@CTTLevel3 IS NULL OR Categories.CTTLevel3 = @CTTLevel3)
		AND (@CTTLevel4 IS NULL OR Categories.CTTLevel4 = @CTTLevel4)
		AND (@CTTLevel5 IS NULL OR Categories.CTTLevel5 = @CTTLevel5)
		AND (@CTTLevel6 IS NULL OR Categories.CTTLevel6 = @CTTLevel6)
		AND (@CTTLevel7 IS NULL OR Categories.CTTLevel7 = @CTTLevel7)
		AND (@CTTLevel8 IS NULL OR Categories.CTTLevel8 = @CTTLevel8)
		AND (@CTTLevel9 IS NULL OR Categories.CTTLevel9 = @CTTLevel9)
		AND (@CTTLevel10 IS NULL OR Categories.CTTLevel10 = @CTTLevel10)
		AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID))AS ChannelsWithRowNumbers
WHERE 
	ChannelsWithRowNumbers.Row >= @StartChannelNo 
	AND ChannelsWithRowNumbers.Row <= @EndChannelNo;
GO
/****** Object:  StoredProcedure [dbo].[dp_getChannelByChannelID]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_getChannelByChannelID]
(
@UserID int,
@ChannelID int
)

AS

SELECT
	Channels.CHANNEL_ID,
	Channels.ChannelName,
	Channels.ImagePath,
	dbo.fn_getChannelPrivacyStatus(@UserID, Channels.CHANNEL_ID) AS PrivacyStatus,
	Channels.bAcceptPasswordRequests
FROM
	Channels
WHERE
	Channels.CHANNEL_ID = @ChannelID
	AND EXISTS (SELECT 1 FROM ChannelsSlides WHERE ChannelsSlides.ChannelID = Channels.CHANNEL_ID);
GO
/****** Object:  StoredProcedure [dbo].[dp_editChannelSlide]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_editChannelSlide]

(
@UserID int,
@ChannelSlideID int,
@URL nvarchar(255) = NULL,
@DisplayDuration float,
@Schedule nvarchar(max),
@PresentationConvertedSchedule nvarchar(4000)
)

AS

IF NOT EXISTS (SELECT 1 FROM ChannelsSlides 
				INNER JOIN Channels ON ChannelsSlides.ChannelID = Channels.CHANNEL_ID 
				INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID)
	RETURN;
	
UPDATE
	ChannelsSlides
SET
	ClickThroughURL = @URL,
	DisplayDuration = @DisplayDuration,
	Schedule = @Schedule,
	PresentationConvertedSchedule = @PresentationConvertedSchedule
WHERE
	CHANNELSSLIDE_ID = @ChannelSlideID;
	
UPDATE
	Channels
SET
	MadeDirtyLastDate = GETDATE()
FROM
	Channels INNER JOIN ChannelsSlides ON Channels.CHANNEL_ID = ChannelsSlides.ChannelID
WHERE
	ChannelsSlides.CHANNELSSLIDE_ID = @ChannelSlideID;
GO
/****** Object:  StoredProcedure [dbo].[dp_addChannelContent]    Script Date: 04/04/2011 13:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[dp_addChannelContent]

(
@UserID int,
@ChannelID int,
@SlideID int,
@Schedule nvarchar(4000),
@PresentationConvertedSchedule nvarchar(300),
@URL nvarchar(255) = NULL,
@DisplayDuration float
)

AS

IF NOT EXISTS (SELECT 1 
				FROM 
					Channels INNER JOIN Publishers ON Channels.PublisherID = Publishers.PUBLISHER_ID
				WHERE
					Publishers.UserID = @UserID)
RETURN;

IF (@URL = '-2')
	SELECT
		@URL = Slides.ClickThroughURL
	FROM
		Slides
	WHERE
		Slides.SLIDE_ID = @SlideID;

IF (@DisplayDuration = -2)
	SELECT
		@DisplayDuration = Slides.DisplayDuration
	FROM
		Slides
	WHERE
		Slides.SLIDE_ID = @SlideID;
		
INSERT INTO ChannelsSlides (ChannelID, SlideID, Schedule, PresentationConvertedSchedule, ClickThroughURL, DisplayDuration)
VALUES(@ChannelID, @SlideID, @Schedule, @PresentationConvertedSchedule, @URL, @DisplayDuration);

UPDATE
	Channels
SET
	MadeDirtyLastDate = GETDATE(),
	ContentLastAddedDate = GETDATE(),
	NoContent = NoContent + 1
WHERE
	CHANNEL_ID = @ChannelID;
GO
/****** Object:  Default [DF_LocationTaxonomyTree_AddDate]    Script Date: 04/04/2011 13:38:32 ******/
ALTER TABLE [dbo].[LocationTaxonomyTree] ADD  CONSTRAINT [DF_LocationTaxonomyTree_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Categories_AddDate]    Script Date: 04/04/2011 13:38:34 ******/
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF_Categories_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_AdvertTaxonomyTrees_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[AdvertTaxonomyTrees] ADD  CONSTRAINT [DF_AdvertTaxonomyTrees_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Users_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Publishers_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Publishers] ADD  CONSTRAINT [DF_Publishers_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Advertisers_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Advertisers] ADD  CONSTRAINT [DF_Advertisers_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Consumers_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Consumers] ADD  CONSTRAINT [DF_Consumers_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Channels_bLocked]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels] ADD  CONSTRAINT [DF_Channels_bLocked]  DEFAULT ((0)) FOR [bLocked]
GO
/****** Object:  Default [DF_Channels_NoContent]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels] ADD  CONSTRAINT [DF_Channels_NoContent]  DEFAULT ((0)) FOR [NoContent]
GO
/****** Object:  Default [DF_Channels_NoFollowers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels] ADD  CONSTRAINT [DF_Channels_NoFollowers]  DEFAULT ((0)) FOR [NoFollowers]
GO
/****** Object:  Default [DF_Channels_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels] ADD  CONSTRAINT [DF_Channels_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_AssetContentFolders_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[AssetContentFolders] ADD  CONSTRAINT [DF_AssetContentFolders_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Adverts_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Adverts] ADD  CONSTRAINT [DF_Adverts_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_SlideFolders_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[SlideFolders] ADD  CONSTRAINT [DF_SlideFolders_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_PCs_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PCs] ADD  CONSTRAINT [DF_PCs_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_PCs_EditDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PCs] ADD  CONSTRAINT [DF_PCs_EditDate]  DEFAULT (getdate()) FOR [EditDate]
GO
/****** Object:  Default [DF_PcChannels_ChannelWeighting]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PcChannels] ADD  CONSTRAINT [DF_PcChannels_ChannelWeighting]  DEFAULT ((10)) FOR [ChannelWeighting]
GO
/****** Object:  Default [DF_Slides_AddDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Slides] ADD  CONSTRAINT [DF_Slides_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_Slides_EditDate]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Slides] ADD  CONSTRAINT [DF_Slides_EditDate]  DEFAULT (getdate()) FOR [EditDate]
GO
/****** Object:  Default [DF_AdvertContents_AddDate]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AdvertContents] ADD  CONSTRAINT [DF_AdvertContents_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  Default [DF_AssetContents_AddDate]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AssetContents] ADD  CONSTRAINT [DF_AssetContents_AddDate]  DEFAULT (getdate()) FOR [AddDate]
GO
/****** Object:  ForeignKey [FK_Channel_Table_Channel_Table]    Script Date: 04/04/2011 13:38:34 ******/
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Channel_Table_Channel_Table] FOREIGN KEY([ParentCategoryID])
REFERENCES [dbo].[Categories] ([CATEGORY_ID])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Channel_Table_Channel_Table]
GO
/****** Object:  ForeignKey [FK_AdvertTaxonomyTrees_AdvertTaxonomyTrees]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[AdvertTaxonomyTrees]  WITH CHECK ADD  CONSTRAINT [FK_AdvertTaxonomyTrees_AdvertTaxonomyTrees] FOREIGN KEY([ParentAdvertTaxonomyTreeID])
REFERENCES [dbo].[AdvertTaxonomyTrees] ([ADVERTTAXONOMYTREE_ID])
GO
ALTER TABLE [dbo].[AdvertTaxonomyTrees] CHECK CONSTRAINT [FK_AdvertTaxonomyTrees_AdvertTaxonomyTrees]
GO
/****** Object:  ForeignKey [FK_Users_AccountType]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_AccountType] FOREIGN KEY([AccountTypeID])
REFERENCES [dbo].[AccountTypes] ([ACCOUNTTYPE_ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_AccountType]
GO
/****** Object:  ForeignKey [FK_Publishers_Users]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Publishers]  WITH CHECK ADD  CONSTRAINT [FK_Publishers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([USER_ID])
GO
ALTER TABLE [dbo].[Publishers] CHECK CONSTRAINT [FK_Publishers_Users]
GO
/****** Object:  ForeignKey [FK_Advertisers_Users]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Advertisers]  WITH CHECK ADD  CONSTRAINT [FK_Advertisers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([USER_ID])
GO
ALTER TABLE [dbo].[Advertisers] CHECK CONSTRAINT [FK_Advertisers_Users]
GO
/****** Object:  ForeignKey [FK_Consumers_SocioEconomicStatus]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Consumers]  WITH CHECK ADD  CONSTRAINT [FK_Consumers_SocioEconomicStatus] FOREIGN KEY([OccupationSectorID])
REFERENCES [dbo].[SocioEconomicStatus] ([SOCIOECONOMICSTATUS_ID])
GO
ALTER TABLE [dbo].[Consumers] CHECK CONSTRAINT [FK_Consumers_SocioEconomicStatus]
GO
/****** Object:  ForeignKey [FK_Consumers_SocioEconomicStatus1]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Consumers]  WITH CHECK ADD  CONSTRAINT [FK_Consumers_SocioEconomicStatus1] FOREIGN KEY([EmploymentLevelID])
REFERENCES [dbo].[SocioEconomicStatus] ([SOCIOECONOMICSTATUS_ID])
GO
ALTER TABLE [dbo].[Consumers] CHECK CONSTRAINT [FK_Consumers_SocioEconomicStatus1]
GO
/****** Object:  ForeignKey [FK_Consumers_SocioEconomicStatus2]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Consumers]  WITH CHECK ADD  CONSTRAINT [FK_Consumers_SocioEconomicStatus2] FOREIGN KEY([AnnualHouseholdIncomeID])
REFERENCES [dbo].[SocioEconomicStatus] ([SOCIOECONOMICSTATUS_ID])
GO
ALTER TABLE [dbo].[Consumers] CHECK CONSTRAINT [FK_Consumers_SocioEconomicStatus2]
GO
/****** Object:  ForeignKey [FK_Consumers_Users]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Consumers]  WITH CHECK ADD  CONSTRAINT [FK_Consumers_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([USER_ID])
GO
ALTER TABLE [dbo].[Consumers] CHECK CONSTRAINT [FK_Consumers_Users]
GO
/****** Object:  ForeignKey [FK_Channels_Categories]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels]  WITH CHECK ADD  CONSTRAINT [FK_Channels_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CATEGORY_ID])
GO
ALTER TABLE [dbo].[Channels] CHECK CONSTRAINT [FK_Channels_Categories]
GO
/****** Object:  ForeignKey [FK_Channels_Publishers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Channels]  WITH CHECK ADD  CONSTRAINT [FK_Channels_Publishers] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publishers] ([PUBLISHER_ID])
GO
ALTER TABLE [dbo].[Channels] CHECK CONSTRAINT [FK_Channels_Publishers]
GO
/****** Object:  ForeignKey [FK_AssetContentFolders_Publishers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[AssetContentFolders]  WITH CHECK ADD  CONSTRAINT [FK_AssetContentFolders_Publishers] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publishers] ([PUBLISHER_ID])
GO
ALTER TABLE [dbo].[AssetContentFolders] CHECK CONSTRAINT [FK_AssetContentFolders_Publishers]
GO
/****** Object:  ForeignKey [FK_Adverts_Advertisers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Adverts]  WITH CHECK ADD  CONSTRAINT [FK_Adverts_Advertisers] FOREIGN KEY([AdvertiserID])
REFERENCES [dbo].[Advertisers] ([ADVERTISER_ID])
GO
ALTER TABLE [dbo].[Adverts] CHECK CONSTRAINT [FK_Adverts_Advertisers]
GO
/****** Object:  ForeignKey [FK_SlideFolders_Publishers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[SlideFolders]  WITH CHECK ADD  CONSTRAINT [FK_SlideFolders_Publishers] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publishers] ([PUBLISHER_ID])
GO
ALTER TABLE [dbo].[SlideFolders] CHECK CONSTRAINT [FK_SlideFolders_Publishers]
GO
/****** Object:  ForeignKey [FK_PCs_Consumers]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PCs]  WITH CHECK ADD  CONSTRAINT [FK_PCs_Consumers] FOREIGN KEY([ConsumerID])
REFERENCES [dbo].[Consumers] ([CONSUMER_ID])
GO
ALTER TABLE [dbo].[PCs] CHECK CONSTRAINT [FK_PCs_Consumers]
GO
/****** Object:  ForeignKey [FK_PcChannels_Channels]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PcChannels]  WITH CHECK ADD  CONSTRAINT [FK_PcChannels_Channels] FOREIGN KEY([ChannelID])
REFERENCES [dbo].[Channels] ([CHANNEL_ID])
GO
ALTER TABLE [dbo].[PcChannels] CHECK CONSTRAINT [FK_PcChannels_Channels]
GO
/****** Object:  ForeignKey [FK_PcChannels_PCs]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[PcChannels]  WITH CHECK ADD  CONSTRAINT [FK_PcChannels_PCs] FOREIGN KEY([PcID])
REFERENCES [dbo].[PCs] ([PC_ID])
GO
ALTER TABLE [dbo].[PcChannels] CHECK CONSTRAINT [FK_PcChannels_PCs]
GO
/****** Object:  ForeignKey [FK_Slides_SlideFolders]    Script Date: 04/04/2011 13:38:37 ******/
ALTER TABLE [dbo].[Slides]  WITH CHECK ADD  CONSTRAINT [FK_Slides_SlideFolders] FOREIGN KEY([SlideFolderID])
REFERENCES [dbo].[SlideFolders] ([SLIDEFOLDER_ID])
GO
ALTER TABLE [dbo].[Slides] CHECK CONSTRAINT [FK_Slides_SlideFolders]
GO
/****** Object:  ForeignKey [FK_AdvertContents_Adverts]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AdvertContents]  WITH CHECK ADD  CONSTRAINT [FK_AdvertContents_Adverts] FOREIGN KEY([AdvertID])
REFERENCES [dbo].[Adverts] ([ADVERT_ID])
GO
ALTER TABLE [dbo].[AdvertContents] CHECK CONSTRAINT [FK_AdvertContents_Adverts]
GO
/****** Object:  ForeignKey [FK_AdvertsAdvertTaxonomyTrees_Adverts]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AdvertsAdvertTaxonomyTrees]  WITH CHECK ADD  CONSTRAINT [FK_AdvertsAdvertTaxonomyTrees_Adverts] FOREIGN KEY([AdvertID])
REFERENCES [dbo].[Adverts] ([ADVERT_ID])
GO
ALTER TABLE [dbo].[AdvertsAdvertTaxonomyTrees] CHECK CONSTRAINT [FK_AdvertsAdvertTaxonomyTrees_Adverts]
GO
/****** Object:  ForeignKey [FK_AdvertsAdvertTaxonomyTrees_AdvertTaxonomyTrees]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AdvertsAdvertTaxonomyTrees]  WITH CHECK ADD  CONSTRAINT [FK_AdvertsAdvertTaxonomyTrees_AdvertTaxonomyTrees] FOREIGN KEY([AdvertTaxonomyTreeID])
REFERENCES [dbo].[AdvertTaxonomyTrees] ([ADVERTTAXONOMYTREE_ID])
GO
ALTER TABLE [dbo].[AdvertsAdvertTaxonomyTrees] CHECK CONSTRAINT [FK_AdvertsAdvertTaxonomyTrees_AdvertTaxonomyTrees]
GO
/****** Object:  ForeignKey [FK_AssetContents_AssetContentFolders]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[AssetContents]  WITH CHECK ADD  CONSTRAINT [FK_AssetContents_AssetContentFolders] FOREIGN KEY([AssetContentFolderID])
REFERENCES [dbo].[AssetContentFolders] ([ASSETCONTENTFOLDER_ID])
GO
ALTER TABLE [dbo].[AssetContents] CHECK CONSTRAINT [FK_AssetContents_AssetContentFolders]
GO
/****** Object:  ForeignKey [FK_ChannelUserPrivacy_Channels]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[ChannelUserPrivacy]  WITH CHECK ADD  CONSTRAINT [FK_ChannelUserPrivacy_Channels] FOREIGN KEY([ChannelID])
REFERENCES [dbo].[Channels] ([CHANNEL_ID])
GO
ALTER TABLE [dbo].[ChannelUserPrivacy] CHECK CONSTRAINT [FK_ChannelUserPrivacy_Channels]
GO
/****** Object:  ForeignKey [FK_ChannelUserPrivacy_Users]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[ChannelUserPrivacy]  WITH CHECK ADD  CONSTRAINT [FK_ChannelUserPrivacy_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([USER_ID])
GO
ALTER TABLE [dbo].[ChannelUserPrivacy] CHECK CONSTRAINT [FK_ChannelUserPrivacy_Users]
GO
/****** Object:  ForeignKey [FK_ChannelsSlides_Channels]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[ChannelsSlides]  WITH CHECK ADD  CONSTRAINT [FK_ChannelsSlides_Channels] FOREIGN KEY([ChannelID])
REFERENCES [dbo].[Channels] ([CHANNEL_ID])
GO
ALTER TABLE [dbo].[ChannelsSlides] CHECK CONSTRAINT [FK_ChannelsSlides_Channels]
GO
/****** Object:  ForeignKey [FK_ChannelsSlides_Slides]    Script Date: 04/04/2011 13:38:38 ******/
ALTER TABLE [dbo].[ChannelsSlides]  WITH CHECK ADD  CONSTRAINT [FK_ChannelsSlides_Slides] FOREIGN KEY([SlideID])
REFERENCES [dbo].[Slides] ([SLIDE_ID])
GO
ALTER TABLE [dbo].[ChannelsSlides] CHECK CONSTRAINT [FK_ChannelsSlides_Slides]
GO
