using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Aurigma.ImageUploader;
using Microsoft.Practices.ServiceLocation;
using Oxigen.ApplicationServices;
using Oxigen.ApplicationServices.Flash;
using Oxigen.Core;
using Oxigen.DurationDetectors;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using AssetContent = OxigenIIAdvertising.SOAStructures.AssetContent;

namespace OxigenIIPresentation
{
    ///////////////
    //
    // As we're uploading only resized images, there is no need to upload the source image but only the resized ones.
    // However, as the Image Uploader is not only used for images, the source file must be uploaded in those cases.
    // A decision to upload the source file depending on the source file type cannot be made 'on the fly' as the source file 
    // is needed before we can check the type of the file. 
    //
    // Similarly, two thumbnails of the file type icon for videos are uploaded and disregarded as we have 
    // another mechanism to extract thumbnails from video.
    // 
    ///////////////
    public partial class Create : System.Web.UI.Page
    {
        private int _minDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["minDisplayDuration"]);
        private int _maxDisplayDuration = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["maxDisplayDuration"]);
        private int _serverTimeout = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["serverTimeout"]);
        private IList<Template> _templates;
        private FileDurationDetectorFactory _fileDurationDetectorFactory = new FileDurationDetectorFactory();

        protected IList<Template> Templates
        {
            get { return _templates; }
        }

        private string _inviteToOverrideFieldText = Resource.InviteToOverrideValues;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = _serverTimeout;

            try
            {
                if (Session["User"] == null)
                    Response.Redirect("Home.aspx");

                ScheduleRepeater.DataSource = new string[] { "", "", "", "", "", "", "", "", "", "" };
                ScheduleRepeater.DataBind();

                UploadTitle.Text = _inviteToOverrideFieldText;
                UploadCreator.Text = _inviteToOverrideFieldText;
                UploadDate.Text = _inviteToOverrideFieldText;
                UploadDescription.Text = _inviteToOverrideFieldText;
                UploadDisplayDuration.Text = _inviteToOverrideFieldText;
                UploadURL.Text = _inviteToOverrideFieldText;

                SetDefaultToTextBox(UploadTitle);
                SetDefaultToTextBox(UploadCreator);
                SetDefaultToTextBox(UploadDate);
                SetDefaultToTextBox(UploadDescription);
                SetDefaultToTextBox(UploadDisplayDuration);
                SetDefaultToTextBox(UploadURL);

                ImageUploader1.LicenseKey = System.Configuration.ConfigurationSettings.AppSettings["imageUploaderLicense"];
                ImageUploader1.FileMask = System.Configuration.ConfigurationSettings.AppSettings["imageUploaderFileMask"];

                OxigenIIAdvertising.SOAStructures.User user = (User)Session["User"];

                if (user == null)
                    return;

                var producer = ServiceLocator.Current.GetInstance<IPublisherManagementService>().GetByUserId(user.UserID);

                UsedBytes.Value = (user.TotalAvailableBytes - user.UsedBytes).ToString();
                BytesBegin.Value = user.UsedBytes.ToString();
                BytesTotal.Value = user.TotalAvailableBytes.ToString();

                string storageCapacity = ((int)((float)user.TotalAvailableBytes / (1024F * 1024F))).ToString();

                StorageCapacity.Text = storageCapacity;
                StorageCapacity2.Text = storageCapacity;

                displayDurationFlashMin.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationFlashMin"];
                displayDurationFlashMax.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationFlashMax"];
                displayDurationVideoMin.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationVideoMin"];
                displayDurationVideoMax.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationVideoMax"];
                displayDurationImageMin.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationImageMin"];
                displayDurationImageMax.Text = System.Configuration.ConfigurationSettings.AppSettings["displayDurationImageMax"];

                ImageUploader1.PaneLayout = PaneLayout.TwoPanes;
                ImageUploader1.TreePaneWidth = 205;
                ImageUploader1.UploadView = Aurigma.ImageUploader.View.AdvancedDetails;
                ImageUploader1.AdvancedDetailsPreviewThumbnailSize = 40;
                ImageUploader1.ButtonSendText = String.Empty;

                ImageUploader1.BackgroundColor = System.Drawing.Color.Transparent;
                ImageUploader1.SplitterLineColor = System.Drawing.Color.Blue;

                ImageUploader1.FolderPaneBorderStyle = Aurigma.ImageUploader.BorderStyle.Fixed3D;
                ImageUploader1.UploadPaneBorderStyle = Aurigma.ImageUploader.BorderStyle.None;
                ImageUploader1.TreePaneBorderStyle = Aurigma.ImageUploader.BorderStyle.Fixed3D;
                ImageUploader1.SplitterLineStyle = LineStyle.Dot;

                ImageUploader1.UploadThumbnail1FitMode = ThumbnailFitMode.Fit;
                ImageUploader1.UploadThumbnail1Width = 100;
                ImageUploader1.UploadThumbnail1Height = 75;
                ImageUploader1.UploadThumbnail1JpegQuality = 60;

                ImageUploader1.UploadThumbnail2FitMode = ThumbnailFitMode.Fit;
                ImageUploader1.UploadThumbnail2Width = 1900;
                ImageUploader1.UploadThumbnail2Height = 1200;
                ImageUploader1.UploadThumbnail2JpegQuality = 75;
                ImageUploader1.UploadThumbnail1ResizeQuality = ResizeQuality.High;

                ImageUploader1.TimeOut = 3600000;
                ImageUploader1.FilesPerOnePackageCount = 1;
                ImageUploader1.AutoRecoverMaxTriesCount = 2;
                ImageUploader1.AutoRecoverTimeOut = 10000;
                ImageUploader1.MaxConnectionCount = 5;
                ImageUploader1.ShowDescriptions = false;
                ImageUploader1.MaxTotalFileSize = (int)((user.TotalAvailableBytes - user.UsedBytes) * 1.1F);
                ImageUploader1.Action = "CreateWizard.aspx";

                _templates = producer.AssignedTemplates;

                //foreach (Template template in templates)
                //    TemplateList.Text += "<option value=\"" + template.Id + "\">" + template.Name + "</option>";

                //  Get total number of uploaded files (all files are uploaded in a single package).
                // (if files have been uploaded)
                if (Request.Form["FileCount"] == null)
                    return;

                long totalUploadSize;

                UploadAndSaveToDB(out totalUploadSize);

                ((User)Session["User"]).UsedBytes += totalUploadSize;
            }
            catch (Exception ex)
            {
                // TODO: handle
                throw ex;
            }
        }

        private void UploadAndSaveToDB(out long totalUploadSize)
        {
            string rawContentPath = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
            string thumbnailAssetContentPath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];

            int userID;
            totalUploadSize = 0;

            if (!Helper.TryGetUserID(Session, out userID))
                return;

            List<AssetContent> assetContents = new List<AssetContent>();

            int fileCount = Int32.Parse(Request.Form["FileCount"]);

            UploadForm uploadForm = new UploadForm(_inviteToOverrideFieldText)
                                    {
                                        Title = Request.Form["TitleOvr"].Trim(),
                                        Creator = Request.Form["CreatorOvr"].Trim(),
                                        Description = Request.Form["DescriptionOvr"].Trim(),
                                        Url = Request.Form["URLOvr"].Trim()
                                    };

            uploadForm.SetDateIfProvided(Request.Form["DateOvr"].Trim());
            uploadForm.SetDisplayDuration(Request.Form["DisplayDurationOvr"].Trim(), _minDisplayDuration, _maxDisplayDuration);

            //Iterate through uploaded data and save the original file and thumbnail
            for (int i = 1; i <= fileCount; i++)
            {
                string newFilename;
                string newFilenameWithoutExtension;
                string newFolder;
                string title;
                DateTime? createdDateTime;
                float mediaDisplayDuration;

                PreviewType previewType;

                if (!uploadForm.UserHasProvidedDate)
                    createdDateTime = ToDotNetFormat(Request.Params["SourceFileCreatedDateTime_" + i]);
                else
                    createdDateTime = uploadForm.Date;

                int contentLength;

                //Get source file and save it to disk.
                HttpPostedFile sourceFile = Request.Files["SourceFile_" + i];
                string fileName = Path.GetFileName(sourceFile.FileName);
                string extension = Path.GetExtension(sourceFile.FileName).ToLower();

                bool bIsImage = GetIsFileImage(extension);
                bool bIsVideo = GetIsFileVideo(extension);

                if (!uploadForm.UserHasProvidedTitle)
                    title = Path.GetFileNameWithoutExtension(fileName);
                else
                    title = uploadForm.Title;

                FilenameMakerLib.FilenameFromGUID.MakeFilenameAndFolder(fileName, out newFilename,
                  out newFilenameWithoutExtension, out newFolder);

                string pathWithoutFilename = rawContentPath + newFolder;

                if (!Directory.Exists(pathWithoutFilename))
                    Directory.CreateDirectory(pathWithoutFilename);

                //Get first thumbnail  and save it to disk.
                HttpPostedFile thumbnail1File = Request.Files["Thumbnail1_" + i];

                string thumbnailFullPhysicalPathWithoutFilename = thumbnailAssetContentPath + newFolder;

                if (!Directory.Exists(thumbnailFullPhysicalPathWithoutFilename))
                    Directory.CreateDirectory(thumbnailFullPhysicalPathWithoutFilename);

                string thumbnailFullPath = thumbnailFullPhysicalPathWithoutFilename + "\\" + newFilenameWithoutExtension + ".jpg";

                if (bIsImage)
                    thumbnail1File.SaveAs(thumbnailFullPath);
                else if (bIsVideo)
                    CopyDefaultThumbnailForType(extension, thumbnailAssetContentPath, thumbnailFullPath);
                else
                {
                    //var swa = new SWAFile(sourceFile.InputStream);
                    //swa.GetLastFrameImageAsThumbnail().Save(thumbnailFullPath);
                    File.Copy(thumbnailAssetContentPath + "flash-swf.jpg", thumbnailFullPath);
                }

                if (uploadForm.UserHasProvidedDisplayDuration)
                    mediaDisplayDuration = uploadForm.DisplayDuration;
                else
                    mediaDisplayDuration = GetDisplayDuration(extension);

                // get resized file and save
                HttpPostedFile thumbnail2File = Request.Files["Thumbnail2_" + i];

                if (bIsImage)
                {
                    thumbnail2File.SaveAs(pathWithoutFilename + "\\" + newFilename);

                    contentLength = thumbnail2File.ContentLength;
                }
                else
                {
                    sourceFile.SaveAs(pathWithoutFilename + "\\" + newFilename);

                    contentLength = sourceFile.ContentLength;
                }

                totalUploadSize += contentLength;

                previewType = GetPreviewType(bIsImage, bIsVideo);

                assetContents.Add(new AssetContent(title,
                  newFolder + "\\" + newFilename,
                  newFilenameWithoutExtension,
                  extension,
                  newFolder + "/" + newFilenameWithoutExtension + ".jpg",
                  newFolder + "\\" + newFilenameWithoutExtension + ".jpg",
                  newFolder,
                  newFilenameWithoutExtension + ".jpg",
                  uploadForm.Description,
                  uploadForm.Creator,
                  createdDateTime,
                  uploadForm.Url,
                  mediaDisplayDuration,
                  contentLength,
                  previewType));
            }

            BLClient client = null;
            bool bDurationsAmended = false;

            try
            {
                client = new BLClient();

                bDurationsAmended = client.AddAssetContent(userID, int.Parse(Request.Form["AssetContentFolderID"]), assetContents);
            }
            finally
            {
                client.Dispose();
            }

            Session.Add("DurationsAmended", bDurationsAmended);
        }

        private float GetDisplayDuration(string path)
        {
            IFileDurationDetector durationDetector = _fileDurationDetectorFactory.CreateDurationDetector(Path.GetExtension(path));
            return (float)durationDetector.GetDurationInSeconds(path);
        }

        private PreviewType GetPreviewType(bool bIsImage, bool bIsVideo)
        {
            if (bIsVideo)
                return PreviewType.Video;

            if (bIsImage)
                return PreviewType.Image;

            return PreviewType.Flash;
        }

        private void CopyDefaultThumbnailForType(string extension, string thumbnailAssetContentPath, string thumbnailFullPath)
        {
            string path = thumbnailAssetContentPath + "video-icon-" + extension.Remove(0, 1) + ".jpg";

            // TODO: method will be obsolete if we find a way to get thumbs from MOV and MP4.
            if (extension == ".avi" || extension == ".mpeg" || extension == ".mpg" || extension == ".wmv")
                return;

            if (File.Exists(path))
            {
                File.Copy(path, thumbnailFullPath);
                return;
            }

            File.Copy(path + "video-icon-default.jpg", thumbnailFullPath);
        }

        private bool GetIsFileImage(string extension)
        {
            return (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".bmp" ||
                extension == ".png" || extension == ".tiff" || extension == ".tif");
        }

        private bool GetIsFileVideo(string extension)
        {
            return (extension == ".avi" || extension == ".mov" || extension == ".mpeg" ||
               extension == ".mpg" || extension == ".wmv" || extension == ".mp4");
        }

        private DateTime? ToDotNetFormat(string date)
        {
            string[] dateComponents = date.Split(new char[] { ':', ' ' });

            return new DateTime(int.Parse(dateComponents[0]), int.Parse(dateComponents[1]), int.Parse(dateComponents[2]));
        }

        private void SetDefaultToTextBox(TextBox textBox)
        {
            textBox.Attributes.Add("onfocus", "clearField(this, '" + _inviteToOverrideFieldText + "')");
            textBox.Attributes.Add("onblur", "fillField(this, '" + _inviteToOverrideFieldText + "')");
        }
    }
}
