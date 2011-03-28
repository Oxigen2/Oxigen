using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	    /// <summary>
    /// A slide's ID property is the only property which is compared to
    /// another slide.  I.e., it does not have any domain signature 
    /// properties other than the Id itself.
    /// </summary>
	[TestFixture]
    public class SlideTests
    {
        [Test]
        public void CanCompareSlides() {
            Slide instance = new Slide();
			EntityIdSetter.SetIdOf<int>(instance, 1);

            Slide instanceToCompareTo = new Slide();
			EntityIdSetter.SetIdOf<int>(instanceToCompareTo, 1);

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
