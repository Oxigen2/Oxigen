SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[dp_getUserMachineChannelSubscriptions]
(
@PCGUID nvarchar(50)
)

AS

DECLARE @PcID int;
DECLARE @UserID int;

SELECT
	@UserID = Consumers.UserID,
	@PcID = PCs.PC_ID
FROM
	PCs INNER JOIN Consumers ON PCs.ConsumerID = Consumers.CONSUMER_ID
WHERE
	PCs.PCGUID = @PCGUID;
	
IF (@@ROWCOUNT<>1)
BEGIN

	RETURN;		

END
	
UPDATE
	PCs
SET
	LastHeartbeatDate = GETDATE()
WHERE
	PCs.PC_ID = @PcID;
	
INSERT INTO HeartbeatHistory(PcID, AddDate) VALUES (@PcID, GETDATE());

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
	PCs.PC_ID = @PcID;