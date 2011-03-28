using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class SlideInstanceFactory
    {
        public static Slide CreateValidTransientSlide() {
            return new Slide() {
			    Filename = "Filename", 
				FilenameExtension = "FilenameExtension", 
				FilenameNoPath = "FilenameNoPath", 
				GUID = "GUID", 
				SubDir = 'a', 
				Name = "Slide Name", 
				Creator = "Creator", 
				Caption = "Caption", 
				ClickThroughURL = "www.google.com", 
				WebsiteURL = "WebsiteURL", 
				DisplayDuration = 10, 
				Length = 15, 
				ImagePath = "ImagePath", 
				ImagePathWinFS = "ImagePathWinFS", 
				ImageName = "ImageFilename", 
				PlayerType = "PlayerType", 
				PreviewType = "PreviewType", 
				bLocked = false,
                UserGivenDate = DateTime.Now,
                AddDate = DateTime.Now,
                EditDate = DateTime.Now,
                MadeDirtyLastDate = DateTime.Now 
            };
        }
    }
}
