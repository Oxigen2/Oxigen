using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OxigenIIAdvertising.SOAStructures;
using System.Threading;
using ImageExtraction;
using System.Drawing;
using System.IO;

namespace OxigenIIAdvertising.Services
{
  public class ImageExtractorThread
  {
    private List<Content> _contents;
    private string _thumbnailPath = null;
    private string _previewFramesPath = null;
    private string _filePath = null;
    private const long _quality = 100;
    private const int _thumbnailWidth = 100;
    private const int _targetNoFrames = 20;

    public ImageExtractorThread(List<Content> contents, MediaType mediaType)
    {
      _contents = contents;

      switch (mediaType)
      {
        case MediaType.RawContent:
          _thumbnailPath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];
          _previewFramesPath = System.Configuration.ConfigurationSettings.AppSettings["previewFramesAssetContentPath"];
          _filePath = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
          break;
        default:
          _thumbnailPath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"];
          _previewFramesPath = System.Configuration.ConfigurationSettings.AppSettings["previewFramesSlidePath"];
          _filePath = System.Configuration.ConfigurationSettings.AppSettings["slidePath"];
          break;
      }
    }

    public void Run()
    {
      Thread thread = new Thread(new ThreadStart(ExtractThumbnails));
      thread.Start();
    }

    private void ExtractThumbnails()
    {
      try
      {
        foreach (Content content in _contents)
        {
            #region if

            if (content.Extension == ".avi" || content.Extension == ".mov" || content.Extension == ".mp4" || content.Extension == ".wmv" || content.Extension == ".mpg" 
            || content.Extension == ".mpeg")
          {
            string previewFrameDirectory = _previewFramesPath + content.SubDir + "\\";

            if (!Directory.Exists(previewFrameDirectory))
              Directory.CreateDirectory(previewFrameDirectory);

            using (VideoFrameReader vfr = new VideoFrameReader(_filePath + content.FileName))
            {
              int noFrames = vfr.NumberOfFrames;

              // save a thumbnail for the first frame for the "My Content" panel
              int firstFrameIndex;

              if (noFrames < 50)
                firstFrameIndex = 0;
              if (noFrames < 200)
                firstFrameIndex = 50;
              else
                firstFrameIndex = 200;

              using (Bitmap firstFrame = vfr.GetFrame(firstFrameIndex))
              {
                int height = (int)((float)_thumbnailWidth * ((float)firstFrame.Height / (float)firstFrame.Width));

                using (Bitmap thumbnail = new Bitmap(_thumbnailWidth, height))
                {
                  using (Graphics g = Graphics.FromImage(thumbnail))
                  {
                    g.DrawImage(firstFrame, 0, 0, _thumbnailWidth, height);
                  }

                  thumbnail.Save(_thumbnailPath + content.SubDir + "\\" + content.FileNameWithoutExtension + ".jpg");
                }
              }

              int frameCount = 1;

              int space = noFrames / _targetNoFrames;

              for (int frameNumber = 0; frameNumber < vfr.NumberOfFrames; frameNumber = frameNumber + space)
              {
                using (Bitmap frame = vfr.GetFrame(frameNumber))
                {
                  ImageUtilities.SaveImage(frame, previewFrameDirectory + content.FileNameWithoutExtension + frameCount + ".jpg", _quality);
                }

                frameCount++;
              }
            }
          }
            #endregion
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }
    }
  }
}
