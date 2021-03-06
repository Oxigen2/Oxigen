using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public class Template : AssetFile
    {
        public Template() {}

        public Template(string ext) : base(ext)
        {
        }

        public virtual string MetaData { get; set; }
        public virtual float DisplayDuration { get; set; }

        protected override string RootFilePath
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["templatePath"]; }
        }

        protected override string RootThumbnailPath
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["thumbnailTemplatePath"]; }
        }

        public virtual Publisher Publisher { get; set; }
    }
}
