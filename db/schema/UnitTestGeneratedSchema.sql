
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK31485DEE2A3A1E7D]') AND parent_object_id = OBJECT_ID('Channels'))
alter table Channels  drop constraint FK31485DEE2A3A1E7D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK22FA883633917BD]') AND parent_object_id = OBJECT_ID('RSSFeeds'))
alter table RSSFeeds  drop constraint FK22FA883633917BD


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK22FA8832A3A1E7D]') AND parent_object_id = OBJECT_ID('RSSFeeds'))
alter table RSSFeeds  drop constraint FK22FA8832A3A1E7D


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK22FA883E8C92243]') AND parent_object_id = OBJECT_ID('RSSFeeds'))
alter table RSSFeeds  drop constraint FK22FA883E8C92243


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK817BB97B2A3A1E7D]') AND parent_object_id = OBJECT_ID('Templates'))
alter table Templates  drop constraint FK817BB97B2A3A1E7D


    if exists (select * from dbo.sysobjects where id = object_id(N'Channels') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Channels

    if exists (select * from dbo.sysobjects where id = object_id(N'Publishers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Publishers

    if exists (select * from dbo.sysobjects where id = object_id(N'RSSFeeds') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RSSFeeds

    if exists (select * from dbo.sysobjects where id = object_id(N'AssetContents') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AssetContents

    if exists (select * from dbo.sysobjects where id = object_id(N'Slides') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Slides

    if exists (select * from dbo.sysobjects where id = object_id(N'Templates') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Templates

    create table Channels (
        Channel_ID INT IDENTITY NOT NULL,
       CategoryID INT null,
       ChannelName NVARCHAR(255) null,
       ChannelGUID NVARCHAR(255) null,
       ChannelDescription NVARCHAR(255) null,
       ChannelLongDescription NVARCHAR(255) null,
       Keywords NVARCHAR(255) null,
       ImagePath NVARCHAR(255) null,
       bHasDefaultThumbnail BIT null,
       bLocked BIT null,
       bAcceptPasswordRequests BIT null,
       ChannelPassword NVARCHAR(255) null,
       ChannelGUIDSuffix NVARCHAR(255) null,
       NoContent INT null,
       NoFollowers INT null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       MadeDirtyLastDate DATETIME null,
       ContentLastAddedDate DATETIME null,
       PublisherID INT null,
       primary key (Channel_ID)
    )

    create table Publishers (
        Publisher_ID INT IDENTITY NOT NULL,
       UserID INT null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       DisplayName NVARCHAR(255) null,
       EmailAddress NVARCHAR(255) null,
       UsedBytes BIGINT null,
       TotalAvailableBytes BIGINT null,
       primary key (Publisher_ID)
    )

    create table RSSFeeds (
        RSSFeed_ID INT IDENTITY NOT NULL,
       URL NVARCHAR(255) null,
       LastChecked DATETIME null,
       LastItem NVARCHAR(255) null,
       SlideFolderID INT null,
       XSLT NVARCHAR(255) null,
       TemplateID INT null,
       PublisherID INT null,
       ChannelID INT null,
       primary key (RSSFeed_ID)
    )

    create table AssetContents (
        AssetContent_ID INT IDENTITY NOT NULL,
       Caption NVARCHAR(255) null,
       UserGivenDate DATETIME null,
       URL NVARCHAR(255) null,
       Creator NVARCHAR(255) null,
       DisplayDuration REAL null,
       Length INT null,
       PreviewType NVARCHAR(255) null,
       Name NVARCHAR(255) null,
       GUID NVARCHAR(255) null,
       Filename NVARCHAR(255) null,
       FilenameNoPath NVARCHAR(255) null,
       FilenameExtension NVARCHAR(255) null,
       ImagePath NVARCHAR(255) null,
       ImagePathWinFS NVARCHAR(255) null,
       SubDir NCHAR(1) null,
       ImageName NVARCHAR(255) null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       primary key (AssetContent_ID)
    )

    create table Slides (
        Slide_ID INT IDENTITY NOT NULL,
       SlideName NVARCHAR(255) null,
       ImageFilename NVARCHAR(255) null,
       Creator NVARCHAR(255) null,
       Caption NVARCHAR(255) null,
       ClickThroughURL NVARCHAR(255) null,
       WebsiteURL NVARCHAR(255) null,
       DisplayDuration REAL null,
       Length INT null,
       PlayerType NVARCHAR(255) null,
       PreviewType NVARCHAR(255) null,
       bLocked BIT null,
       UserGivenDate DATETIME null,
       MadeDirtyLastDate DATETIME null,
       SlideFolderID INT null,
       GUID NVARCHAR(255) null,
       Filename NVARCHAR(255) null,
       FilenameNoPath NVARCHAR(255) null,
       FilenameExtension NVARCHAR(255) null,
       ImagePath NVARCHAR(255) null,
       ImagePathWinFS NVARCHAR(255) null,
       SubDir NCHAR(1) null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       primary key (Slide_ID)
    )

    create table Templates (
        Template_ID INT IDENTITY NOT NULL,
       MetaData NVARCHAR(255) null,
       Name NVARCHAR(255) null,
       GUID NVARCHAR(255) null,
       Filename NVARCHAR(255) null,
       FilenameNoPath NVARCHAR(255) null,
       FilenameExtension NVARCHAR(255) null,
       ImagePath NVARCHAR(255) null,
       ImagePathWinFS NVARCHAR(255) null,
       SubDir NCHAR(1) null,
       ImageName NVARCHAR(255) null,
       AddDate DATETIME null,
       EditDate DATETIME null,
       PublisherID INT null,
       primary key (Template_ID)
    )

    alter table Channels 
        add constraint FK31485DEE2A3A1E7D 
        foreign key (PublisherID) 
        references Publishers

    alter table RSSFeeds 
        add constraint FK22FA883633917BD 
        foreign key (TemplateID) 
        references Templates

    alter table RSSFeeds 
        add constraint FK22FA8832A3A1E7D 
        foreign key (PublisherID) 
        references Publishers

    alter table RSSFeeds 
        add constraint FK22FA883E8C92243 
        foreign key (ChannelID) 
        references Channels

    alter table Templates 
        add constraint FK817BB97B2A3A1E7D 
        foreign key (PublisherID) 
        references Publishers
