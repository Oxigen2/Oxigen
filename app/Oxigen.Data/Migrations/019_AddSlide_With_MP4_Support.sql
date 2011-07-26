SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_addSlide]
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
	
IF (@AssetFilenameExtension IN ('.mov', '.mp4'))
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