
    if exists (select * from dbo.sysobjects where id = object_id(N'AssetContents') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AssetContents

    if exists (select * from dbo.sysobjects where id = object_id(N'Slides') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Slides

    create table AssetContents (
        ASSETCONTENT_ID INT IDENTITY NOT NULL,
       Name NVARCHAR(255) null,
       Caption NVARCHAR(255) null,
       GUID NVARCHAR(255) null,
       Filename NVARCHAR(255) null,
       FilenameNoPath NVARCHAR(255) null,
       FilenameExtension NVARCHAR(255) null,
       ImagePath NVARCHAR(255) null,
       ImagePathWinFS NVARCHAR(255) null,
       SubDir NCHAR(1) null,
       ImageName NVARCHAR(255) null,
       UserGivenDate DATETIME null,
       URL NVARCHAR(255) null,
       Creator NVARCHAR(255) null,
       DisplayDuration REAL null,
       Length INT null,
       PreviewType NVARCHAR(255) null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       primary key (ASSETCONTENT_ID)
    )

    create table Slides (
        SLIDE_ID INT IDENTITY NOT NULL,
       Filename NVARCHAR(255) null,
       FilenameExtension NVARCHAR(255) null,
       FilenameNoPath NVARCHAR(255) null,
       GUID NVARCHAR(255) null,
       SubDir NCHAR(1) null,
       SlideName NVARCHAR(255) null,
       Creator NVARCHAR(255) null,
       Caption NVARCHAR(255) null,
       ClickThroughURL NVARCHAR(255) null,
       WebsiteURL NVARCHAR(255) null,
       DisplayDuration REAL null,
       Length INT null,
       ImagePath NVARCHAR(255) null,
       ImagePathWinFS NVARCHAR(255) null,
       ImageFilename NVARCHAR(255) null,
       PlayerType NVARCHAR(255) null,
       PreviewType NVARCHAR(255) null,
       bLocked BIT null,
       UserGivenDate DATETIME null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       MadeDirtyLastDate DATETIME null,
       primary key (SLIDE_ID)
    )
