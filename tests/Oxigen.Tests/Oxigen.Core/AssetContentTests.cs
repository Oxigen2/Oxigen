using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	    /// <summary>
    /// A assetContent's ID property is the only property which is compared to
    /// another assetContent.  I.e., it does not have any domain signature 
    /// properties other than the Id itself.
    /// </summary>
	[TestFixture]
    public class AssetContentTests
    {
        [Test]
        public void CanCompareAssetContents() {
            AssetContent instance = new AssetContent();
			EntityIdSetter.SetIdOf<int>(instance, 1);

            AssetContent instanceToCompareTo = new AssetContent();
			EntityIdSetter.SetIdOf<int>(instanceToCompareTo, 1);

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
