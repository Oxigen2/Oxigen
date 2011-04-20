ALTER PROCEDURE [dbo].[dp_removeChannelContent]

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
	MadeDirtyLastDate = GETDATE()
WHERE
	CHANNEL_ID = @ChannelID;
GO

ALTER PROCEDURE [dbo].[dp_addChannelContent]

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
	ContentLastAddedDate = GETDATE()
WHERE
	CHANNEL_ID = @ChannelID;
GO

CREATE TRIGGER [dbo].[trChannelsSlides] 
   ON  [dbo].[ChannelsSlides]
   AFTER INSERT
AS 
BEGIN

SET NOCOUNT ON;

update Channels
set NoContent = Channels.NoContent + ChannelsSlidesGroups.ChannelSlideCount
from Channels inner join
(select COUNT(*) as ChannelSlideCount, ChannelID
from inserted
group by ChannelID)
as ChannelsSlidesGroups on ChannelsSlidesGroups.ChannelID = Channels.CHANNEL_ID

END
GO 
CREATE TRIGGER [dbo].[trDeleteChannelsSlides] 
   ON  [dbo].[ChannelsSlides]
   AFTER DELETE
AS 
BEGIN

SET NOCOUNT ON;

update Channels
set NoContent = Channels.NoContent - ChannelsSlidesGroups.ChannelSlideCount
from Channels inner join
(select COUNT(*) as ChannelSlideCount, ChannelID
from deleted
group by ChannelID)
as ChannelsSlidesGroups on ChannelsSlidesGroups.ChannelID = Channels.CHANNEL_ID

END

GO

CREATE PROCEDURE mt_UpdateNoContentInChannels
AS
BEGIN
update Channels
set NoContent = ChannelsSlidesGroups.ChannelSlideCount
from Channels inner join
(select COUNT(ChannelID) as ChannelSlideCount, CHANNEL_ID
from Channels left join ChannelsSlides on Channels.CHANNEL_ID = ChannelsSlides.ChannelID
group by CHANNEL_ID)
as ChannelsSlidesGroups on ChannelsSlidesGroups.CHANNEL_ID = Channels.CHANNEL_ID

END

GO

exec mt_UpdateNoContentInChannels