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
        protected string _rawContentPath;
        protected string _thumbnailAssetContentPath;
        protected Stream _uploadedStream;
        protected Stream _thumbnail1Stream;
        protected Stream _thumbnail2Stream;
        protected string _thumbnailFullPath;
        protected string _pathWithoutFilename;
        protected string _destinationFullPath;
        private string _thumbnailFullPhysicalPathWithoutFilename;
        private readonly FileDurationDetectorFactory _fileDurationDetectorFactory;
        private string _guidFilenameWithExtension;
        protected int _contentLength;

        public UploadedFile(UploadForm uploadForm, FileDurationDetectorFactory fileDurationDetectorFactory)
        {
            _uploadForm = uploadForm;
            _fileDurationDetectorFactory = fileDurationDetectorFactory;
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

        public Stream UploadedStream
        {
            set { _uploadedStream = value; }
        }

        public string OriginalFileName
        {
            set 
            {
                _originalFilename = Path.GetFileName(value);
                _filenameWithoutExtension = Path.GetFileNameWithoutExtension(_originalFilename);
                _extension = Path.GetExtension(value).ToLower();
                _title = GetFilenameOrUserDefinedTitle(_filenameWithoutExtension);
                FilenameMakerLib.FilenameFromGUID.MakeFilenameAndFolder(_originalFilename, out _guidFilenameWithoutExtension, out _folder);
                _pathWithoutFilename = _rawContentPath + _folder;
                _guidFilenameWithExtension = _guidFilenameWithoutExtension + _extension;
                _destinationFullPath = _pathWithoutFilename + "\\" + _guidFilenameWithoutExtension + _extension;
                _thumbnailFullPhysicalPathWithoutFilename = _thumbnailAssetContentPath + _folder;
                _thumbnailFullPath = _thumbnailFullPhysicalPathWithoutFilename + "\\" + _guidFilenameWithoutExtension + ".jpg";
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
                _date = _uploadForm.Date;
            else
                _date = ConvertDateFromUploadedFormat(stringDate);
        }

        private DateTime? ConvertDateFromUploadedFormat(string date)
        {
            string[] dateComponents = date.Split(new char[] { ':', ' ' });

            return new DateTime(int.Parse(dateComponents[0]), int.Parse(dateComponents[1]), int.Parse(dateComponents[2]));
        }

        public Stream Thumbnail1Stream
        {
            set { _thumbnail1Stream = value; }
        }

        public Stream Thumbnail2Stream
        {
            set { _thumbnail2Stream = value; }
        }

        public virtual int ContentLength
        {
            get { return _contentLength; }
        }
        
        public string GuidFilenameWithExtension
        {
            get
            {
                return _guidFilenameWithExtension;
            }
            set
            {
                _guidFilenameWithExtension = value;
            }
        }

        public string RawContentPath
        {
            set { _rawContentPath = value; }
        }

        public string ThumbnailAssetContentPath
        {
            set { _thumbnailAssetContentPath = value; }
        }

        public abstract PreviewType PreviewType { get; }

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
            if (!File.Exists(_destinationFullPath))
                throw new FileNotFoundException("File " + _destinationFullPath + " does not exist.");

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
            _contentLength = (int)_uploadedStream.Length;
            SaveStream(_uploadedStream, _destinationFullPath);
        }

        protected void SaveStream(Stream inputStream, string destinationFullPath)
        {
            using (var reader = new BinaryReader(inputStream))
            {
                const int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                const int offset = 0;

                using (var writingStream = new FileStream(destinationFullPath, System.IO.FileMode.OpenOrCreate))
                {
                    int count;
                    while ((count = reader.Read(buffer, offset, buffer.Length)) > 0)
                    {
                        writingStream.Write(buffer, offset, count);
                    }
                    writingStream.Flush();
                    writingStream.Close();
                }
                reader.Close();
            }
        }

        public abstract void SaveThumbnail();

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
            SaveStream(_thumbnail1Stream, _thumbnailFullPath);
        }

        public override void SaveContent()
        {
            CreateContentFolderIfNotExists();
            _contentLength = (int)_thumbnail2Stream.Length;
            SaveStream(_thumbnail2Stream, _destinationFullPath);
        }

        public override int ContentLength
        {
            get { return _contentLength; }
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