﻿using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using NUnit.Framework;
using Oxigen.DurationDetectors;
using OxigenIIAdvertising.SOAStructures;
using OxigenIIPresentation;

namespace Oxigen.Tests32
{
    [TestFixture, Category("IntegrationTest")]
    public class UploadSaveFilesTest
    {
        private const string _inviteToOverrideAutoValues = "Default";
        private const string _rawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
        private const string _thumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
        FileDurationDetectorFactory _durationDetectorFactory = new FileDurationDetectorFactory();
        private double TIME_THRESHOLD_SECONDS = 1;

        [SetUp]
        public void Start()
        {
            if (!Directory.Exists(_rawContentPath))
                Directory.CreateDirectory(_rawContentPath);

            if (!Directory.Exists(_thumbnailAssetContentPath))
                Directory.CreateDirectory(_thumbnailAssetContentPath);

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-avi.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-avi.jpg", _thumbnailAssetContentPath + "video-icon-avi.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-default.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-default.jpg", _thumbnailAssetContentPath + "video-icon-default.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-mov.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-mov.jpg", _thumbnailAssetContentPath + "video-icon-mov.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-mp4.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-mp4.jpg", _thumbnailAssetContentPath + "video-icon-mp4.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-mpeg.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-mpeg.jpg", _thumbnailAssetContentPath + "video-icon-mpeg.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-mpg.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-mpg.jpg", _thumbnailAssetContentPath + "video-icon-mpg.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "video-icon-wmv.jpg"))
                File.Copy("../../Resources/GenericThumbnails/video-icon-wmv.jpg", _thumbnailAssetContentPath + "video-icon-wmv.jpg");

            if (!File.Exists(_thumbnailAssetContentPath + "flash-swf.jpg"))
                File.Copy("../../Resources/GenericThumbnails/flash-swf.jpg", _thumbnailAssetContentPath + "flash-swf.jpg");
        }

        [Test]
        public void CanSaveAndGetInfoSWF_NoUserData()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleSWF))
            {
                file = new UploadedFlashFile(form, _durationDetectorFactory);
                file.RawContentPath = _rawContentPath;
                file.ThumbnailAssetContentPath = _thumbnailAssetContentPath;
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_SWF.swf";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_SWF", file.Title);
                Assert.AreEqual(".swf", file.Extension);
                Assert.AreEqual(new DateTime(2011, 7, 8), file.Date);
                Assert.AreEqual(115009, file.ContentLength);
                Assert.AreEqual(PreviewType.Flash, file.PreviewType);
                Assert.AreEqual(-1, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoSWF_UserSetDateAndDuration_UserDateHonoured_DurationHonoured()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleSWF))
            {
                file = new UploadedFlashFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_SWF.swf";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_SWF", file.Title);
                Assert.AreEqual(".swf", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(115009, file.ContentLength);
                Assert.AreEqual(PreviewType.Flash, file.PreviewType);
                Assert.AreEqual(100f, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoMOV_NoUserData()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleMOV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_MOV.mov";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_MOV", file.Title);
                Assert.AreEqual(".mov", file.Extension);
                Assert.AreEqual(new DateTime(2011, 7, 8), file.Date);
                Assert.AreEqual(3284257, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                AssertWithThreshold(85f, file.DisplayDuration, TIME_THRESHOLD_SECONDS);
            }
        }

        [Test]
        public void CanSaveAndGetInfoMOV_UserSetDateAndDuration_UserDateHonoured_DurationHonoured()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleMOV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_MOV.mov";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_MOV", file.Title);
                Assert.AreEqual(".mov", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(3284257, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                Assert.AreEqual(100f, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoWMV_NoUserData()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleWMV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_WMV.wmv";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_WMV", file.Title);
                Assert.AreEqual(".wmv", file.Extension);
                Assert.AreEqual(new DateTime(2011, 7, 8), file.Date);
                Assert.AreEqual(30972789, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                AssertWithThreshold(150f, file.DisplayDuration, TIME_THRESHOLD_SECONDS);
            }
        }

        [Test]
        public void CanSaveAndGetInfoWMV_UserSetDateAndDuration_UserDateHonoured_DurationHonoured()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleWMV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_WMV.wmv";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_WMV", file.Title);
                Assert.AreEqual(".wmv", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(30972789, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                Assert.AreEqual(100, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoJPG_NoUserData()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream())
            {
                TestFiles.sampleJPG.Save(ms, ImageFormat.Jpeg);

                file = new UploadedImageFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_JPG.jpg";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_JPG", file.Title);
                Assert.AreEqual(".jpg", file.Extension);
                Assert.AreEqual(new DateTime(2011, 7, 8), file.Date);
                Assert.AreEqual(1184183, file.ContentLength);
                Assert.AreEqual(PreviewType.Image, file.PreviewType);
                Assert.AreEqual(-1, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoJPG_UserSetDateAndDuration_UserDateHonoured_DurationHonoured()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            // Act
            using (MemoryStream ms = new MemoryStream())
            {
                TestFiles.sampleJPG.Save(ms, ImageFormat.Jpeg);

                file = new UploadedImageFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_JPG.jpg";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_JPG", file.Title);
                Assert.AreEqual(".jpg", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(PreviewType.Image, file.PreviewType);
                Assert.AreEqual(100, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveAndGetInfoPNG_UserSetDateAndDuration_UserDateHonoured_DurationHonoured()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            UploadedFile file;

            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            // Act
            using (MemoryStream ms = new MemoryStream())
            {
                TestFiles.SamplePNG.Save(ms, ImageFormat.Png);

                file = new UploadedImageFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_PNG.png";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_PNG", file.Title);
                Assert.AreEqual(".png", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(PreviewType.Image, file.PreviewType);
                Assert.AreEqual(100, file.DisplayDuration);
            }
        }

        [Test]
        public void CanSaveVideoFileWithUserDataEmptyDateEmptyTitleEmptyDuration()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            form.Title = "";
            form.SetDateIfProvided("");
            form.SetDisplayDuration("", 1, 10);
            
            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleWMV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_WMV.wmv";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("Sample_WMV", file.Title);
                Assert.AreEqual(".wmv", file.Extension);
                Assert.AreEqual(new DateTime(2011, 7, 8), file.Date);
                Assert.AreEqual(30972789, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                AssertWithThreshold(150f, file.DisplayDuration, TIME_THRESHOLD_SECONDS);
            }
        }

        [Test]
        public void CanSaveVideoFileWithUserData()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);
            form.Title = "The Title";
            form.SetDisplayDuration("100", 5, 200);
            form.SetDateIfProvided("2011-12-20");

            UploadedFile file;

            // Act
            using (MemoryStream ms = new MemoryStream(TestFiles.sampleWMV))
            {
                file = new UploadedVideoFile(form, _durationDetectorFactory);
                file.RawContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Assets\";
                file.ThumbnailAssetContentPath = @"C:\Oxigen2\Oxigen\tests\TestRepository\Thumbnails\";
                file.UploadedStream = ms;
                file.OriginalFileName = "Sample_WMV.wmv";
                file.SetDateIfUserHasNotProvidedOne("2011 7 8");

                SaveThumbnail(file);

                // Assert
                Assert.AreEqual("The Title", file.Title);
                Assert.AreEqual(".wmv", file.Extension);
                Assert.AreEqual(new DateTime(2011, 12, 20), file.Date);
                Assert.AreEqual(30972789, file.ContentLength);
                Assert.AreEqual(PreviewType.Video, file.PreviewType);
                Assert.AreEqual(100f, file.DisplayDuration);
            }
        }

        private void AssertWithThreshold(double expectedDuration, double actualDuration, double timeThreshold)
        {
            if (Math.Abs(expectedDuration - actualDuration) <= timeThreshold)
                Assert.Pass("Deviation between actual and expected values is within the threshold of " + timeThreshold + " seconds. Expected value was " + expectedDuration + ", actual is " + actualDuration);

            Assert.Fail("Deviation between between actual and expected values is outside the threshold of " + timeThreshold + " seconds. Expected value was " + expectedDuration + ", actual is " + actualDuration);
        }

        private static void SaveThumbnail(UploadedFile file)
        {
            using (MemoryStream thumbnail1Stream = new MemoryStream())
            {
                TestFiles.sampleJPG.Save(thumbnail1Stream, ImageFormat.Jpeg);

                using (MemoryStream thumbnail2Stream = new MemoryStream())
                {
                    TestFiles.sampleJPG.Save(thumbnail2Stream, ImageFormat.Jpeg);
                    file.Thumbnail1Stream = thumbnail1Stream;
                    file.Thumbnail2Stream = thumbnail2Stream;

                    file.SaveThumbnail();
                    file.SaveContent();
                    file.SetDisplayDuration();
                }
            }
        }

        [TearDown]
        public void CleanUp()
        {
            if (Directory.Exists(_rawContentPath))
                Directory.Delete(_rawContentPath, true);

            if (Directory.Exists(_thumbnailAssetContentPath))
                Directory.Delete(_thumbnailAssetContentPath, true);
        }
    }
}