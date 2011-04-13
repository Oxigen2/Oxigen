using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	[TestFixture]
    public class SlideFolderTests
    {
        [Test]
        public void CanCompareSlideFolders() {
            SlideFolder instance = new SlideFolder();
			instance.SlideFolderName = "Slide Folder 1";
			instance.Publisher = null;

            SlideFolder instanceToCompareTo = new SlideFolder();
			instanceToCompareTo.SlideFolderName = "Slide Folder 1";
			instanceToCompareTo.Publisher = null;

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
