using System;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System.Collections.Generic;

namespace Oxigen.Core
{

        public class Slide : AssetFile
    {
        public Slide()
        {
        }

        public Slide(string extension) : base(extension)
        {
            MadeDirtyLastDate = DateTime.Now;
            bLocked = false;
        }

		public virtual string Creator { get; set; }

		public virtual string Caption { get; set; }

		public virtual string ClickThroughURL { get; set; }

		public virtual string WebsiteURL { get; set; }

		public virtual float DisplayDuration { get; set; }

		public virtual int Length { get; set; }

		public virtual string PlayerType { get; set; }

		public virtual string PreviewType { get; set; }

		public virtual bool bLocked { get; set; }

		public virtual DateTime? UserGivenDate { get; set; }

		public virtual DateTime? MadeDirtyLastDate { get; set; }

        public virtual int SlideFolderID { get; set; }

        public virtual IList<ChannelsSlide> AssignedChannels { get; set; }

        protected override string RootFilePath
            {
                get { return System.Configuration.ConfigurationSettings.AppSettings["slidePath"]; }
            }

        protected override string RootThumbnailPath
            {
                get { return System.Configuration.ConfigurationSettings.AppSettings["thumbnailSlidePath"]; }
            }
    }
}
