BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.SlideFolders ADD
	MaxSlideCount int NOT NULL CONSTRAINT DF_SlideFolders_se DEFAULT 0
ALTER TABLE dbo.SlideFolders ADD
	SlideCount int NOT NULL CONSTRAINT DF_SlideFolders_sc DEFAULT 0
GO
ALTER TABLE dbo.SlideFolders SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

CREATE TRIGGER [dbo].[trInsertSlides] 
   ON  [dbo].[Slides]
   AFTER INSERT
AS 
BEGIN

SET NOCOUNT ON;

update SlideFolders
set SlideCount = SlideFolders.SlideCount + NewSlideGroups.SlideCount
from SlideFolders inner join
(select COUNT(*) as SlideCount, SlideFolderID
from inserted
group by SlideFolderID)
as NewSlideGroups on NewSlideGroups.SlideFolderID = SlideFolders.SLIDEFOLDER_ID

END
GO 
CREATE TRIGGER [dbo].[trDeleteSlides] 
   ON  [dbo].[Slides]
   AFTER DELETE
AS 
BEGIN

SET NOCOUNT ON;

update SlideFolders
set SlideCount = SlideFolders.SlideCount - SlideGroups.SlideCount
from SlideFolders inner join
(select COUNT(*) as SlideCount, SlideFolderID
from deleted
group by SlideFolderID)
as SlideGroups on SlideGroups.SlideFolderID = SlideFolders.SLIDEFOLDER_ID

END

GO

CREATE PROCEDURE mt_UpdateSlideCountInFolders
AS
BEGIN
update SlideFolders
set SlideCount = NewSlideGroups.SlideCount
from SlideFolders inner join
(select COUNT(SlideFolderID) as SlideCount, SLIDEFOLDER_ID
from SlideFolders left join Slides on SlideFolders.SLIDEFOLDER_ID = Slides.SlideFolderID
group by SLIDEFOLDER_ID)
as NewSlideGroups on NewSlideGroups.SLIDEFOLDER_ID = SlideFolders.SLIDEFOLDER_ID

END

GO

exec mt_UpdateSlideCountInFolders