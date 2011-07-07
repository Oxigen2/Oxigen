using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using Aurigma.ImageUploader;
using Microsoft.Practices.ServiceLocation;
using Oxigen.ApplicationServices;
using Oxigen.Core;
using Oxigen.DurationDetectors;
using OxigenIIAdvertising.BLClients;
using OxigenIIAdvertising.SOAStructures;
using log4net;
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
        private string _rawContentPath = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
        private string _thumbnailAssetContentPath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];
        private IList<Template> _templates;
        private FileDurationDetectorFactory _fileDurationDetectorFactory = new FileDurationDetectorFactory();
        private UploadedFileFactory _uploadedFileFactory = new UploadedFileFactory();
        private ILog _logger = LogManager.GetLogger("Logger1");

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
                _logger.Error(ex.ToString());
            }
        }

        private void UploadAndSaveToDB(out long totalUploadSize)
        {
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
                HttpPostedFile postedFile = Request.Files["SourceFile_" + i];
                UploadedFile uploadedFile = _uploadedFileFactory.CreateUploadedFile(uploadForm, _fileDurationDetectorFactory, Path.GetExtension(postedFile.FileName).ToLower());

                uploadedFile.RawContentPath = _rawContentPath;
                uploadedFile.ThumbnailAssetContentPath = _thumbnailAssetContentPath;
                uploadedFile.UploadedStream = postedFile.InputStream;
                uploadedFile.OriginalFileName = postedFile.FileName;
                uploadedFile.SetDateIfUserHasNotProvidedOne(Request.Params["SourceFileCreatedDateTime_" + i]);
                
                uploadedFile.Thumbnail1Stream = Request.Files["Thumbnail1_" + i].InputStream;
                uploadedFile.Thumbnail2Stream = Request.Files["Thumbnail2_" + i].InputStream;
                
                uploadedFile.SaveThumbnail();
                uploadedFile.SaveContent();
                uploadedFile.SetDisplayDuration();

                totalUploadSize += uploadedFile.ContentLength;

                assetContents.Add(new AssetContent(uploadedFile.Title,
                  uploadedFile.Folder + "\\" + uploadedFile.GuidFilenameWithExtension,
                  uploadedFile.GuidFilenameWithoutExtension,
                  uploadedFile.Extension,
                  uploadedFile.Folder + "/" + uploadedFile.GuidFilenameWithoutExtension + ".jpg",
                  uploadedFile.Folder + "\\" + uploadedFile.GuidFilenameWithoutExtension + ".jpg",
                  uploadedFile.Folder,
                  uploadedFile.GuidFilenameWithoutExtension + ".jpg",
                  uploadForm.Description,
                  uploadForm.Creator,
                  uploadedFile.Date,
                  uploadForm.Url,
                  uploadedFile.DisplayDuration,
                  uploadedFile.ContentLength,
                  uploadedFile.PreviewType));
            }

            BLClient client = null;
            bool bDurationsAmended;

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

       private void SetDefaultToTextBox(TextBox textBox)
        {
            textBox.Attributes.Add("onfocus", "clearField(this, '" + _inviteToOverrideFieldText + "')");
            textBox.Attributes.Add("onblur", "fillField(this, '" + _inviteToOverrideFieldText + "')");
        }
    }
}
