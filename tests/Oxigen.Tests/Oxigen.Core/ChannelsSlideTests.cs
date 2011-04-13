using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	[TestFixture]
    public class ChannelsSlideTests
    {
        [Test]
        public void CanCompareChannelsSlides() {
            ChannelsSlide instance = new ChannelsSlide();
			instance.Channel = null;
			instance.Slide = null;

            ChannelsSlide instanceToCompareTo = new ChannelsSlide();
			instanceToCompareTo.Channel = null;
			instanceToCompareTo.Slide = null;

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
