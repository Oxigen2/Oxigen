using NUnit.Framework;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
	    /// <summary>
    /// A template's ID property is the only property which is compared to
    /// another template.  I.e., it does not have any domain signature 
    /// properties other than the Id itself.
    /// </summary>
	[TestFixture]
    public class TemplateTests
    {
        [Test]
        public void CanCompareTemplates() {
            Template instance = new Template();
			EntityIdSetter.SetIdOf<int>(instance, 1);

            Template instanceToCompareTo = new Template();
			EntityIdSetter.SetIdOf<int>(instanceToCompareTo, 1);

			instance.ShouldEqual(instanceToCompareTo);
        }
    }
}
