using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class SlideFolderInstanceFactory
    {
        public static SlideFolder CreateValidTransientSlideFolder() {
            return new SlideFolder() {
			    SlideFolderName = "Slide Folder 1", 
				Publisher = null 
            };
        }
    }
}
