using System.Windows.Forms;
using NUnit.Framework;
using Setup;

namespace Oxigen.Tests32
{
    [TestFixture]
    public class InstallationPrerequisitesTest
    {
        [Test]
        public void InsufficientRam()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                RamPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            RAMPrerequisite ramPrerequisite = new RAMPrerequisite(new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsFalse(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.DoesNotExist, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void BetweenMandatoryAndRecommendedRam()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                RamPrerequisiteStatus = PrerequisiteStatus.BetweenMandatoryAndRecommended
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            RAMPrerequisite ramPrerequisite = new RAMPrerequisite(new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.BetweenMandatoryAndRecommended, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void RecommendedOrAboveRam()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                RamPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            RAMPrerequisite ramPrerequisite = new RAMPrerequisite(new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.Exists, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void DoesNotHaveDotNet35()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                DotNet35PrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            DotNet35Prerequisite ramPrerequisite = new DotNet35Prerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsFalse(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.DoesNotExist, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void HasDotNet35()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                DotNet35PrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            DotNet35Prerequisite ramPrerequisite = new DotNet35Prerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.Exists, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void DoesNotHaveFlash()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            FlashActiveXPerequisite ramPrerequisite = new FlashActiveXPerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsFalse(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.DoesNotExist, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void HasFlash()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            FlashActiveXPerequisite ramPrerequisite = new FlashActiveXPerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.Exists, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void DoesNotHaveWMP()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                WMPPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            WMPPrerequisite ramPrerequisite = new WMPPrerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.DoesNotExist, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void HasWMP()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                WMPPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            DotNet35Prerequisite ramPrerequisite = new DotNet35Prerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.Exists, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void DoesNotHaveQuickTime()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                QTPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            QTPrerequisite ramPrerequisite = new QTPrerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.DoesNotExist, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void HasQuicktime()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                QTPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            QTPrerequisite ramPrerequisite = new QTPrerequisite(new LinkLabel(), new PictureBox());
            collection.Add(ramPrerequisite);

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
            Assert.AreEqual(PrerequisiteStatus.Exists, ramPrerequisite.PrerequisiteStatus);
        }

        [Test]
        public void HasOneMandatoryDoesNotHaveOneOptional()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                QTPrerequisiteStatus = PrerequisiteStatus.DoesNotExist,
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            collection.Add(new QTPrerequisite(new LinkLabel(), new PictureBox()));
            collection.Add(new FlashActiveXPerequisite(new LinkLabel(), new PictureBox()));
            
            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
        }

        [Test]
        public void HasAllProvidedPrerequisites()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                QTPrerequisiteStatus = PrerequisiteStatus.Exists,
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            collection.Add(new QTPrerequisite(new LinkLabel(), new PictureBox()));
            collection.Add(new FlashActiveXPerequisite(new LinkLabel(), new PictureBox()));

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsTrue(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
        }

        [Test]
        public void DoesNotHaveOneMandatoryHasOneOptional()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                QTPrerequisiteStatus = PrerequisiteStatus.Exists,
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.DoesNotExist
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            collection.Add(new QTPrerequisite(new LinkLabel(), new PictureBox()));
            collection.Add(new FlashActiveXPerequisite(new LinkLabel(), new PictureBox()));

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsFalse(collection.CanContinueWithInstallation);
        }

        [Test]
        public void RamAboveMinimumBelowRecommendedHasOneMandatoryCanInstall()
        {
            // Arrange
            IInstallationPrerequisiteProvider installationPrerequisiteProvider = new MockInstallationPrerequisiteProvider()
            {
                RamPrerequisiteStatus = PrerequisiteStatus.BetweenMandatoryAndRecommended,
                FlashActiveXPrerequisiteStatus = PrerequisiteStatus.Exists
            };

            InstallationPrerequisiteCollection collection = new InstallationPrerequisiteCollection(installationPrerequisiteProvider);

            collection.Add(new RAMPrerequisite(new PictureBox()));
            collection.Add(new FlashActiveXPerequisite(new LinkLabel(), new PictureBox()));

            // Act
            collection.CheckAllPrerequisites();

            // Assert
            Assert.IsFalse(collection.PrerequisitesFullyMet);
            Assert.IsTrue(collection.CanContinueWithInstallation);
        }
    }
}
