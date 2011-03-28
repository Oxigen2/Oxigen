using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class AssetContentInstanceFactory
    {
        public static AssetContent CreateValidTransientAssetContent() {
            return new AssetContent() {
			    Name = "Picture", 
				Caption = "Hello", 
				GUID = "GUID",
                Filename = "Filename",
                FilenameNoPath = "FilenameNoPath",
                FilenameExtension = "FilenameExtension",
                ImagePath = "ImagePath",
                ImagePathWinFS = "ImagePathWinFS",
                SubDir = 'a',
                ImageName = "ImageName",
                UserGivenDate = DateTime.Now,
                URL = "URL",
                Creator = "Creator",
                DisplayDuration = 5.0F,
                Length = 10,
                PreviewType = "PreviewType",
                AddDate = DateTime.Now,
                EditDate = DateTime.Now         
            };
        }
    }
}
