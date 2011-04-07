using NUnit.Framework;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	[TestFixture]
    public class PublisherTests
    {
        [Test]
        public void CanComparePublishers() {
            Publisher instance = new Publisher();
			instance.UserID = 3;

            Publisher instanceToCompareTo = new Publisher();
			instanceToCompareTo.UserID = 3;

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
