using System;
using NUnit.Framework;
using OxigenIIPresentation;

namespace Oxigen.Tests32
{
    [TestFixture]
    public class UploadFormTests
    {
        private const string _inviteToOverrideAutoValues = "Default";

        [Test]
        public void CanGetDataFromAllFilledInAndLegit()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.Title = "Title";
            form.Description = "Description";
            form.Creator = "Creator";
            form.Url = "url";
            form.SetDateIfNotEmpty("11-01-2011");
            form.SetDisplayDuration("12.5", 11, 13);

            // Assert
            Assert.AreEqual("Title", form.Title);
            Assert.AreEqual("Description", form.Description);
            Assert.AreEqual("Creator", form.Creator);
            Assert.AreEqual("url", form.Url);
            Assert.AreEqual(new DateTime(2011, 1, 11), form.Date);
            Assert.AreEqual(12.5, form.DisplayDuration);
            Assert.IsTrue(form.UserHasProvidedDate);
            Assert.IsTrue(form.UserHasProvidedDisplayDuration);
            Assert.IsTrue(form.UserHasProvidedTitle);
        }

        [Test]
        public void StringPropertiesShouldBeNull()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.Title = "";
            form.Description = "";
            form.Creator = "";
            form.Url = "";

            // Assert
            Assert.IsNull(form.Title);
            Assert.IsNull(form.Description);
            Assert.IsNull(form.Creator);
            Assert.IsNull(form.Url);
        }

        [Test]
        public void StringInputNullUserDataBoolsShouldBeFalse()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.Title = "";
            form.Description = "";
            form.Creator = "";
            form.Url = "";

            // Assert
            Assert.IsFalse(form.UserHasProvidedDate);
            Assert.IsFalse(form.UserHasProvidedDisplayDuration);
            Assert.IsFalse(form.UserHasProvidedTitle);
        }

        [Test]
        public void DateShouldBeNull()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.SetDateIfNotEmpty("");

            // Assert
            Assert.IsNull(form.Date);
            Assert.IsFalse(form.UserHasProvidedDate);
        }

        [Test]
        public void DisplayDurationShouldBeUserDefined()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.SetDisplayDuration("", 10, 20);

            // Assert
            Assert.AreEqual(form.DisplayDuration, -1);
            Assert.IsFalse(form.UserHasProvidedDisplayDuration);
        }

        [Test]
        public void NonStringInputNullNonStringUserDataBoolsShouldBeFalse()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.SetDateIfNotEmpty("");
            form.SetDisplayDuration("", 10, 20);

            // Assert
            Assert.IsFalse(form.UserHasProvidedDate);
            Assert.IsFalse(form.UserHasProvidedDisplayDuration);
        }

        [Test]
        public void DisplayDurationOutsideBoundariesShouldBeSetToMinusOne()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.SetDisplayDuration("120.2", 10, 20);
            
            // Assert
            Assert.AreEqual(form.DisplayDuration, -1);
            Assert.IsTrue(form.UserHasProvidedDisplayDuration);
        }

        [Test]
        public void DisplayDurationWithinBoundaries()
        {
            // Arrange
            UploadForm form = new UploadForm(_inviteToOverrideAutoValues);

            // Act
            form.SetDisplayDuration("120.2", 10, 200);

            // Assert
            Assert.AreEqual(form.DisplayDuration, 120.2F);
            Assert.IsTrue(form.UserHasProvidedDisplayDuration);
        }
    }
}
