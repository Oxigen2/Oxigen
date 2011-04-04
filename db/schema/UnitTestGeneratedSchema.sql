
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK817BB97B75BA5B83]') AND parent_object_id = OBJECT_ID('Templates'))
alter table Templates  drop constraint FK817BB97B75BA5B83


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK817BB97B72590719]') AND parent_object_id = OBJECT_ID('Templates'))
alter table Templates  drop constraint FK817BB97B72590719


    if exists (select * from dbo.sysobjects where id = object_id(N'Publishers') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Publishers

    if exists (select * from dbo.sysobjects where id = object_id(N'AssetContents') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table AssetContents

    if exists (select * from dbo.sysobjects where id = object_id(N'Slides') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Slides

    if exists (select * from dbo.sysobjects where id = object_id(N'Templates') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Templates

    create table Publishers (
        Id_ID INT IDENTITY NOT NULL,
       UserID INT null,
       FirstName NVARCHAR(255) null,
       LastName NVARCHAR(255) null,
       DisplayName NVARCHAR(255) null,
       EmailAddress NVARCHAR(255) null,
       UsedBytes BIGINT null,
       TotalAvailableBytes BIGINT null,
       primary key (Id_ID)
    )

    create table AssetContents (
        Id_ID INT IDENTITY NOT NULL,
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
       primary key (Id_ID)
    )

    create table Slides (
        Id_ID INT IDENTITY NOT NULL,
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
       primary key (Id_ID)
    )

    create table Templates (
        Id_ID INT IDENTITY NOT NULL,
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
       OwnedByFk INT null,
       PublisherFk INT null,
       primary key (Id_ID)
    )

    alter table Templates 
        add constraint FK817BB97B75BA5B83 
        foreign key (OwnedByFk) 
        references Publishers

    alter table Templates 
        add constraint FK817BB97B72590719 
        foreign key (PublisherFk) 
        references Publishers
