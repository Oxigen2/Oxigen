﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;

namespace Oxigen.Core
{
    public abstract class AssetFile : AuditEntity
    {
        private char GetRandomLetter() {
            string rand = Path.GetRandomFileName();

            short alpha = (short)'A';
            short zed = (short)'Z';

            foreach (char c in rand)
            {
                char cUpper = c.ToString().ToUpper()[0];

                if ((short)cUpper >= alpha && (short)cUpper <= zed)
                    return cUpper;
            }

            return 'Z';
        }

        public AssetFile ()
        {
        }

        public AssetFile(string extension)
        {
            GUID = Guid.NewGuid().ToString().ToUpper() + "_" + GetRandomLetter();
            FilenameExtension = extension;
            FilenameNoPath = GUID + FilenameExtension;
            SubDir = GUID[GUID.Length - 1];
            Filename = SubDir + @"\" + FilenameNoPath;
            ImageName = GUID + ".jpg";
            ImagePath = SubDir + @"/" + ImageName;
            ImagePathWinFS = SubDir + @"\" + ImageName;

            //Check directories exist
            if (!Directory.Exists(RootFilePath + SubDir))
                Directory.CreateDirectory(RootFilePath + SubDir);
            if (!Directory.Exists(RootThumbnailPath + SubDir))
                Directory.CreateDirectory(RootThumbnailPath + SubDir);

            AddDate = DateTime.Now;
            EditDate = DateTime.Now;
        }

        public virtual string Name { get; set; }

        [DomainSignature]
        [NotNull]
        public virtual string GUID { get; set; }
        [NotNull]
        public virtual string Filename { get; set; }
        [NotNull]
        public virtual string FilenameNoPath { get; set; }
        [NotNull]
        public virtual string FilenameExtension { get; set; }
        [NotNull]
        public virtual string ImagePath { get; set; }
        [NotNull]
        public virtual string ImagePathWinFS { get; set; }

        public virtual char? SubDir { get; set; }
        public virtual string ImageName { get; set; }

        protected abstract string RootFilePath { get;}
        protected abstract string RootThumbnailPath { get; }

        public virtual string FileFullPathName
        {
            get { return RootFilePath + Filename; }
        }
        public virtual string ThumbnailFullPathName
        {
            get { return RootThumbnailPath + ImagePathWinFS; }
        }
    }
}
