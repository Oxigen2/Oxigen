using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class AssetContent : AssetFile
    {

        public virtual string Caption { get; set; }
        public virtual DateTime?  UserGivenDate { get; set; }
        public virtual string URL { get; set; }
        public virtual string Creator { get; set; }

        public virtual float DisplayDuration { get; set; }      
        public virtual int Length { get; set; }
        [NotNull]
        public virtual string PreviewType { get; set; }

        protected override string RootFilePath
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["assetContentPath"]; }
        }

        protected override string RootThumbnailPath
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["thumbnailAssetContentPath"]; }
        }
    }

}
