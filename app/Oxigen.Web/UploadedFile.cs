using System;
using System.IO;
using System.Web;
using Oxigen.DurationDetectors;
using OxigenIIAdvertising.SOAStructures;

namespace OxigenIIPresentation
{
    public abstract class UploadedFile
    {
        private readonly UploadForm _uploadForm;
        private string _originalFilename;
        protected string _extension;
        private string _filenameWithoutExtension;
        protected string _guidFilenameWithoutExtension;
        private string _folder;
        private string _title;
        private DateTime? _date;
        private float _displayDuration;
        protected HttpPostedFile _postedFile;
        protected HttpPostedFile _thumbnail1;
        protected HttpPostedFile _thumbnail2;
        private string _rawContentPath = System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"];
        protected string _thumbnailAssetContentPath = System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"];
        protected string _thumbnailFullPath;
        protected string _pathWithoutFilename;
        protected string _destinationFullPath;
        private string _thumbnailFullPhysicalPathWithoutFilename;
        private FileDurationDetectorFactory _fileDurationDetectorFactory;
        private string _guidFilenameWithExtension;

        public UploadedFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory)
        {
            _uploadForm = uploadForm;
            _fileDurationDetectorFactory = fileDurationDetectorFactory;
        }

        public string OriginalFilename
        {
            get { return _originalFilename; }
        }

        public string FilenameWithoutExtension
        {
            get { return _filenameWithoutExtension; }
        }

        public string Folder
        {
            get { return _folder; }
        }

        public string Title
        {
            get { return _title; }
        }

        public DateTime? Date
        {
            get { return _date; }
        }

        public float DisplayDuration
        {
            get { return _displayDuration; }
        }

        public string GuidFilenameWithoutExtension
        {
            get { return _guidFilenameWithoutExtension; }
        }

        public string Extension
        {
            get { return _extension; }
        }

        public HttpPostedFile PostedFile
        {
            get { return _postedFile; }
            set
            {
                _postedFile = value;
                _originalFilename = Path.GetFileName(value.FileName);
                _filenameWithoutExtension = Path.GetFileNameWithoutExtension(_originalFilename);
                _extension = Path.GetExtension(value.FileName).ToLower();
                _title = GetFilenameOrUserDefinedTitle(_filenameWithoutExtension);
                FilenameMakerLib.FilenameFromGUID.MakeFilenameAndFolder(_originalFilename, out _guidFilenameWithoutExtension, out _folder);
                _pathWithoutFilename = _rawContentPath + _folder;
                _guidFilenameWithExtension = _guidFilenameWithoutExtension + _extension;
                _destinationFullPath = _pathWithoutFilename + "\\" + _guidFilenameWithoutExtension + _extension;
            }
        }

        private string GetFilenameOrUserDefinedTitle(string filenameWithoutExtension)
        {
            if (!_uploadForm.UserHasProvidedTitle)
                return Path.GetFileNameWithoutExtension(filenameWithoutExtension);

            return _uploadForm.Title;
        }

        public void SetDateIfUserHasNotProvidedOne(string stringDate)
        {
            if (_uploadForm.UserHasProvidedDate)
                _date = GetDateFromUploadedFile(stringDate);
            else
                _date = _uploadForm.Date;
        }

        private DateTime? GetDateFromUploadedFile(string date)
        {
            string[] dateComponents = date.Split(new char[] { ':', ' ' });

            return new DateTime(int.Parse(dateComponents[0]), int.Parse(dateComponents[1]), int.Parse(dateComponents[2]));
        }

        public string PathWithoutFilename
        {
            get { return _pathWithoutFilename; }
        }

        public HttpPostedFile Thumbnail1
        {
            get { return _thumbnail1; }
            set
            {
                _thumbnail1 = value;
                _thumbnailFullPhysicalPathWithoutFilename = _thumbnailAssetContentPath + _folder;
                _thumbnailFullPath = _thumbnailFullPhysicalPathWithoutFilename + "\\" + _guidFilenameWithoutExtension + ".jpg";
            }
        }

        public HttpPostedFile Thumbnail2
        {
            set { _thumbnail2 = value; }
        }

        protected void CreateThumbnailFolderIfNotExists()
        {
            if (!Directory.Exists(_thumbnailFullPhysicalPathWithoutFilename))
                Directory.CreateDirectory(_thumbnailFullPhysicalPathWithoutFilename);
        }

        protected void CreateContentFolderIfNotExists()
        {
            if (!Directory.Exists(_pathWithoutFilename))
                Directory.CreateDirectory(_pathWithoutFilename);
        }

        public void SetDisplayDuration()
        {
            if (File.Exists(_destinationFullPath))
            {
                throw new FileNotFoundException("File " + _pathWithoutFilename + "\\" + _destinationFullPath +
                                                " does not exist. Please save this " + GetType() +
                                                " before setting the display duration.");
            }

            if (_uploadForm.UserHasProvidedDisplayDuration)
                _displayDuration = _uploadForm.DisplayDuration;
            else
            {
                IFileDurationDetector durationDetector = _fileDurationDetectorFactory.CreateDurationDetector(_extension);
                _displayDuration = (float)durationDetector.GetDurationInSeconds(_destinationFullPath);
            }
        }

        public virtual void SaveContent()
        {
            CreateContentFolderIfNotExists();
            _postedFile.SaveAs(_destinationFullPath);
        }

        public virtual int ContentLength
        {
            get { return _postedFile.ContentLength; }
        }

        public abstract void SaveThumbnail();
        public abstract PreviewType PreviewType { get; }

        public string DestinationFullPath
        {
            get { return _destinationFullPath; }
            set { _destinationFullPath = value; }
        }

        public string GuidFilenameWithExtension
        {
            get {
                return _guidFilenameWithExtension;
            }
            set {
                _guidFilenameWithExtension = value;
            }
        }
    }

    public class UploadedFlashFile : UploadedFile
    {
        public UploadedFlashFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory)
            : base(uploadForm, fileDurationDetectorFactory)
        {
        }
        
        public override void SaveThumbnail()
        {
            CreateThumbnailFolderIfNotExists();
            File.Copy(_thumbnailAssetContentPath + "flash-swf.jpg", _thumbnailFullPath);
        }

        public override PreviewType PreviewType
        {
            get { return PreviewType.Flash; }
        }
    }

    public class UploadedVideoFile : UploadedFile
    {
        public UploadedVideoFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory)
            : base(uploadForm, fileDurationDetectorFactory)
        {
        }
        
        public override void SaveThumbnail()
        {
            CreateThumbnailFolderIfNotExists();

            string path = _thumbnailAssetContentPath + "video-icon-" + _extension.Remove(0, 1) + ".jpg";

            if (HaveMeansToExtractThumbnailsFromFormat(_extension))
                return; // Thumbnails will be extracted later on the Business Logic Layer

            if (File.Exists(path))
            {
                File.Copy(path, _thumbnailFullPath);
                return;
            }

            File.Copy(path + "video-icon-default.jpg", _thumbnailFullPath);
        }

        public override PreviewType PreviewType
        {
            get { return PreviewType.Video; }
        }

        private static bool HaveMeansToExtractThumbnailsFromFormat(string extension)
        {
            // we have implemented thumbnail extraction for these formats
            return extension == ".avi" || extension == ".mpeg" || extension == ".mpg" || extension == ".wmv";
        }
    }

    public class UploadedImageFile : UploadedFile
    {
        public UploadedImageFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory)
            : base(uploadForm, fileDurationDetectorFactory)
        {
        }
        
        public override void SaveThumbnail()
        {
            CreateThumbnailFolderIfNotExists();
            _thumbnail1.SaveAs(_thumbnailFullPath);
        }

        public override void SaveContent()
        {
            CreateContentFolderIfNotExists();
            _thumbnail2.SaveAs(_destinationFullPath);
        }

        public override int ContentLength
        {
            get { return _thumbnail2.ContentLength; }
        }

        public override PreviewType PreviewType
        {
            get { return PreviewType.Image; }
        }
    }

    public class UploadedFileFactory
    {
        public UploadedFile CreateUploadedFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory, string extension)
        {
            if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".bmp" ||
                extension == ".png" || extension == ".tiff" || extension == ".tif")
                return new UploadedImageFile(uploadForm, fileDurationDetectorFactory);

            if (extension == ".avi" || extension == ".mov" || extension == ".mpeg" ||
               extension == ".mpg" || extension == ".wmv" || extension == ".mp4")
                return new UploadedVideoFile(uploadForm, fileDurationDetectorFactory);

            return new UploadedFlashFile(uploadForm, fileDurationDetectorFactory);
        }
    }
}