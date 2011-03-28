using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Aurigma.ImageUploader;

namespace OxigenIIPresentation
{
  public partial class Uploader : System.Web.UI.Page
  {
    private string galleryPath = "Gallery/";

    protected void Page_Load(object sender, EventArgs e)
    {
      //First of all, clear data uploaded at previous time.
      if (string.IsNullOrEmpty(Request.Form["PackageGuid"]))
        InitGallery();

      ImageUploader1.LicenseKey = System.Configuration.ConfigurationSettings.AppSettings["imageUploaderLicense"];

      ImageUploader1.FileMask = System.Configuration.ConfigurationSettings.AppSettings["imageUploaderFileMask"];

			ImageUploader1.PaneLayout = PaneLayout.TwoPanes;
			ImageUploader1.TreePaneWidth = 205;
			ImageUploader1.ShowButtons = false;
			ImageUploader1.ShowDebugWindow = true;
      ImageUploader1.UploadView = Aurigma.ImageUploader.View.AdvancedDetails;
      ImageUploader1.AdvancedDetailsPreviewThumbnailSize = 40;

			ImageUploader1.BackgroundColor = System.Drawing.Color.Transparent;
			ImageUploader1.SplitterLineColor = System.Drawing.Color.Blue;

			ImageUploader1.FolderPaneBorderStyle = Aurigma.ImageUploader.BorderStyle.Fixed3D;
			ImageUploader1.UploadPaneBorderStyle = Aurigma.ImageUploader.BorderStyle.None;
			ImageUploader1.TreePaneBorderStyle = Aurigma.ImageUploader.BorderStyle.Fixed3D;
			ImageUploader1.SplitterLineStyle = LineStyle.Dot;

			ImageUploader1.UploadThumbnail1FitMode = ThumbnailFitMode.Fit;
			ImageUploader1.UploadThumbnail1Width = 1200;
			ImageUploader1.UploadThumbnail1Height = 1200;
			ImageUploader1.UploadThumbnail1JpegQuality = 60;
			ImageUploader1.UploadThumbnail1CopyExif = true;

			ImageUploader1.UploadThumbnail2FitMode = ThumbnailFitMode.Fit;
			ImageUploader1.UploadThumbnail2Width = 120;
			ImageUploader1.UploadThumbnail2Height = 120;
      ImageUploader1.UploadThumbnail2JpegQuality = 60;

			ImageUploader1.MaxFileCount = 10;
			ImageUploader1.MaxTotalFileSize = 52428800;

			ImageUploader1.FilesPerOnePackageCount = 1;
			ImageUploader1.AutoRecoverMaxTriesCount = 10;
			ImageUploader1.AutoRecoverTimeOut = 10000;
      ImageUploader1.MaxConnectionCount = 5;

      ImageUploader1.Action = "Uploader.aspx?handlerID=1;Uploader.aspx?handlerID=2;Uploader.aspx?handlerID=3;Uploader.aspx?handlerID=4;Uploader.aspx?handlerID=5";

			ImageUploader1.RedirectUrl = "Gallery.aspx";

			ImageUploader1.FileUploaded += new FileUploadEventHandler(ImageUploader1_FileUploaded); 
    }

    public void ImageUploader1_FileUploaded(object sender, FileUploadEventArgs e)
    {
      //Save file and thumbnail		
      string physGalleryPath = Server.MapPath(galleryPath);
      string sourceFileName = e.SourceFile.GetSafeFileName(physGalleryPath);
      e.SourceFile.Save(System.IO.Path.Combine(physGalleryPath, sourceFileName));

      string physThumbnailsPath = Server.MapPath(galleryPath + "Thumbnails/");
      string thumbnailFileName = e.Thumbnails[1].GetSafeFileName(physThumbnailsPath);
      e.Thumbnails[1].Save(System.IO.Path.Combine(physThumbnailsPath, thumbnailFileName));

      //Save file info.
      lock (Application) 
      {
        XmlDocument descriptions = new XmlDocument();
        descriptions.Load(Server.MapPath(galleryPath + "Descriptions.xml"));

        XmlElement file = descriptions.CreateElement("file");
        file.SetAttribute("name", sourceFileName);
        file.SetAttribute("thumbName", thumbnailFileName);
        file.SetAttribute("width", Convert.ToString(e.SourceFile.Width));
        file.SetAttribute("height", Convert.ToString(e.SourceFile.Height));
        file.SetAttribute("description", e.Description);
        file.SetAttribute("handlerUrl", Request.Url.PathAndQuery);

        descriptions.DocumentElement.AppendChild(file);

        descriptions.Save(Server.MapPath(galleryPath + "Descriptions.xml"));
      }
    }

    private void InitGallery()
    {
      //Delete source files
      DirectoryInfo dir = new DirectoryInfo(Server.MapPath(galleryPath));
      foreach (FileInfo file in dir.GetFiles())
      {
        file.Delete();
      }

      //Delete thumbnails
      dir = new DirectoryInfo(Server.MapPath(galleryPath) + "/Thumbnails");
      foreach (FileInfo file in dir.GetFiles())
      {
        file.Delete();
      }

      XmlDocument descriptions = new XmlDocument();

      descriptions.AppendChild(descriptions.CreateElement("files"));

      descriptions.DocumentElement.SetAttribute("totalUploadedFileSize", "0");
      descriptions.DocumentElement.SetAttribute("totalSourceFileSize", Request.Form["TotalSourceFileSize"]);

      descriptions.Save(Server.MapPath(galleryPath + "Descriptions.xml"));
    }
  }
}
