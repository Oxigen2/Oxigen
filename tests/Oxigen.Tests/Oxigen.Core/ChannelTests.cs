using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	[TestFixture]
    public class ChannelTests
    {
        [Test]
        public void CanCompareChannel() {
            Channel instance = new Channel();
			instance.ChannelGUID = "0FEB4452-E74D-4B89-B4B2-2ED667024878_A";

            Channel instanceToCompareTo = new Channel();
			instanceToCompareTo.ChannelGUID = "0FEB4452-E74D-4B89-B4B2-2ED667024878_A";

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
