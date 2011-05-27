ALTER TABLE PCs	ADD LastHeartbeatDate DATETIME, LastClickDate DATETIME, LastImpressionDate DATETIME;

/****** Object:  Table [dbo].[HeartbeatHistory]    Script Date: 05/24/2011 15:00:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HeartbeatHistory](
	[HEARTBEATHISTORY_ID] [bigint] IDENTITY(1000,1) NOT NULL,
	[PcID] [int] NOT NULL,
	[AddDate] [datetime] NOT NULL,
 CONSTRAINT [PK_HeartbeatHistory] PRIMARY KEY CLUSTERED 
(
	[HEARTBEATHISTORY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HeartbeatHistory]  WITH CHECK ADD  CONSTRAINT [FK_HeartbeatHistory_PCs] FOREIGN KEY([PcID])
REFERENCES [dbo].[PCs] ([PC_ID])
GO

ALTER TABLE [dbo].[HeartbeatHistory] CHECK CONSTRAINT [FK_HeartbeatHistory_PCs]
GO


ALTER TABLE [dbo].[HeartbeatHistory]  WITH CHECK ADD  CONSTRAINT [FK_HeartbeatHistory_PCs] FOREIGN KEY([PcID])
REFERENCES [dbo].[PCs] ([PC_ID])
GO

ALTER TABLE [dbo].[HeartbeatHistory] CHECK CONSTRAINT [FK_HeartbeatHistory_PCs]
GO

/****** Object:  StoredProcedure [dbo].[dp_getUserMachineChannelSubscriptions]    Script Date: 05/24/2011 11:37:52 ******/
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