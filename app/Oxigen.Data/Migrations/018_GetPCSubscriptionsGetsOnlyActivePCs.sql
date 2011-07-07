SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_getPcSubscriptions]
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
	Consumers.UserID = @UserID
	AND PCs.bDeleted = 0;