using System;
using Oxigen.Core;
using Oxigen.Core.Installer;
using SharpArch.Testing;
using SharpArch.Testing.NUnit;
using NUnit.Framework;

namespace Tests.Oxigen.Core.Installer
{
    [TestFixture]
    public class SetupFileTests
    {
        [Test]
        public void CanCreateSingleLineTest()
        {
            var setupFile = new InstallerSetup();
            setupFile.Add(10, "GUID", "Stream Name", 20);

            var setupText = setupFile.GetSetupText();

            setupText.ShouldEqual("10,,GUID,,Stream Name,,20\r\n");
        }

        [Test]
        public void CanCreateMultiLineTest()
        {
            var setupFile = new InstallerSetup();
            setupFile.Add(10, "GUID", "Stream Name", 20);
            setupFile.Add(30, "GUID2", "Stream Name 2", 30);

            var setupText = setupFile.GetSetupText();

            setupText.ShouldEqual("10,,GUID,,Stream Name,,20\r\n30,,GUID2,,Stream Name 2,,30\r\n");
        }

        [Test]
        public void CanCreateEmptyTest()
        {
            var setupFile = new InstallerSetup();
            var setupText = setupFile.GetSetupText();
            setupText.ShouldEqual("");
        }
    }
}
