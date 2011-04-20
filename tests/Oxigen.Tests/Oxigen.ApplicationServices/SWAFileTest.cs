using NUnit.Framework;
using Oxigen.ApplicationServices.Flash;

namespace Tests.Oxigen.ApplicationServices
{
    [TestFixture]
    public class SWAFileTests
    {
        
        [Test]
        [Ignore]
        public void CanSeeLinkedText()
        {
            var file = new SWAFile(@"C:\Users\Ashter\Desktop\Guardian_20110420_3.swf");
            file.UpdateText("TitleTest", "Test");

        }
    }
}
