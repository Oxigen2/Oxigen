using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageExtraction
{
  public class ImageUtilities
  {
    /// <summary>Saves an image to a specified path and with a specified quality, if appropriate to the format.</summary>
    /// <param name="bmp">The image to be saved.</param>
    /// <param name="path">The path where to save the image.</param>
    /// <param name="quality">The quality of the image to save.</param>
    public static void SaveImage(Bitmap bmp, string path, long quality)
    {
      // Save it to a file format based on the path's extension
      switch (Path.GetExtension(path).ToLowerInvariant())
      {
        default:
        case ".bmp": bmp.Save(path, ImageFormat.Bmp); break;
        case ".png": bmp.Save(path, ImageFormat.Png); break;
        case ".gif": bmp.Save(path, ImageFormat.Gif); break;
        case ".tif":
        case ".tiff": bmp.Save(path, ImageFormat.Tiff); break;
        case ".jpg":
          using (EncoderParameters codecParams = new EncoderParameters(1))
          using (EncoderParameter ratio = new EncoderParameter(Encoder.Quality, quality))
          {
            // Set the JPEG quality value and save the image
            codecParams.Param[0] = ratio;
            bmp.Save(path, _jpegCodec, codecParams);
          }
          break;
      }
    }

    /// <summary>The JPEG ImageCodecInfo.  This should always be available.</summary>
    private static ImageCodecInfo _jpegCodec = Array.Find(
        ImageCodecInfo.GetImageEncoders(),
        delegate(ImageCodecInfo ici) { return ici.MimeType == "image/jpeg"; });


    public static Image Crop(Image imgPhoto, int Width, int Height, AnchorPosition anchor) {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent;
            float nPercentW;
            float nPercentH;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW) {
                nPercent = nPercentW;
                switch (anchor) {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)
                            (Height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)((Height - (sourceHeight * nPercent)) / 2);

                        break;
                }
            }
            else {
                nPercent = nPercentH;
                switch (anchor) {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)
                          (Width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)((Width - (sourceWidth * nPercent)) / 2);

                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(Width,
                    Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            // HACK: Get rid of the halo
            if (anchor == AnchorPosition.Center) {
                destX -= 1;
                destY -= 1;
                destWidth += 2;
                destHeight += 2;
            }

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

  }

   
   public enum AnchorPosition {
        Top,
        Bottom,
        Left,
        Right,
        Center
    }
  }
