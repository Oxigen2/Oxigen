using System.IO;
using NUnit.Framework;
using OxigenIIAdvertising.ContentExchanger;

namespace Oxigen.Tests32
{
    [TestFixture]
    public class FailedInternetConnectionAttemptRegistryAccessorIntegrationTests
    {
        private IFailedInternetConnectionAttemptAccessor accessor;

        [SetUp]
        public void Initialize()
        {
            accessor = new FailedInternetConnectionAttemptFileAccessor();
           FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath = () => @"C:\Oxigen2\Oxigen\tests\TestRepository\failed_internet_connection_attempts.txt";

           if (File.Exists(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath()))
               File.Delete(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath());
        }

        [Test]
        public void CanRecordToNewFile()
        {
            // Act
            accessor.RecordFailedAttempt();

            // Assert
            Assert.AreEqual(1, accessor.GetFailedAttempts());
        }

        [Test]
        public void CanAddToExistingFile()
        {
            // Arrange
            File.WriteAllText(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath(), "7");

            // Act
            accessor.RecordFailedAttempt();

            // Assert
            Assert.AreEqual(8, accessor.GetFailedAttempts());
        }

        [Test]
        public void CanDeleteUnparseableFileAndReturnsZeroAttemptsWhenModifying()
        {
            // Arrange
            File.WriteAllText(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath(), "not an integer");

            // Act
            accessor.RecordFailedAttempt();
            
            // Assert
            Assert.AreEqual(1, accessor.GetFailedAttempts());
        }

        [Test]
        public void CanDeleteUnparseableFileAndReturnsZeroAttemptsWhenQuerying()
        {
            // Arrange
            File.WriteAllText(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath(), "not an integer");

            // Act, Assert
            Assert.AreEqual(0, accessor.GetFailedAttempts());
        }

        [Test]
        public void CanReset()
        {
            // Arrange
            File.WriteAllText(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath(), "12");

            // Act
            accessor.ResetFailedAttempts();

            // Assert
            Assert.AreEqual(0, accessor.GetFailedAttempts());
        }

        [TearDown]
        public void CleanUp()
        {
            if (File.Exists(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath()))
                File.Delete(FailedInternetConnectionAttemptFileAccessor.Config.FailedInternetConnectionAttemptsFilePath());
        }
    }
}
