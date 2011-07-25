using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OxigenIIAdvertising.ContentExchanger;

namespace Oxigen.Tests32
{
    [TestFixture, Category("IntegrationTest")]
    public class ContentExchangerTempToPermFileMoverTests
    {
        private const string _filePath = @".\";

        #region Setup/Teardown
        [SetUp]
        public void CreateMockFile()
        {
            if (!File.Exists(_filePath + "sample.dat1"))
                TryCreateMockFileUntilSuccessful();

            if (File.Exists(_filePath + "sample.dat"))
                File.Delete(_filePath + "sample.dat");
        }

        private void TryCreateMockFileUntilSuccessful()
        {
            try
            {
                File.WriteAllText(_filePath + "sample.dat1", "mock contents"); // as we check if file size is greater than zero, write mock contents
            }
            catch (IOException)
            {
                TryCreateMockFileUntilSuccessful();
            }
        }

        [TearDown]
        public void CleanUp()
        {
            if (File.Exists(_filePath + "sample.dat"))
                TryCleaningUpUntilSuccessful(_filePath + "sample.dat");

            if (File.Exists(_filePath + "sample.dat1"))
                TryCleaningUpUntilSuccessful(_filePath + "sample.dat1");
        }

        private void TryCleaningUpUntilSuccessful(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (IOException)
            {
                TryCleaningUpUntilSuccessful(filePath);
            }
        }

        #endregion

        [Test]
        public void SaveFile()
        {
            if (!File.Exists(_filePath + "sample.dat1"))
                File.Create(_filePath + "sample.dat1");

            ITempToPermFileMover mover = new TempToPermFileMover();

            mover.TryMoveFromTempToPerm(_filePath + "sample.dat1");

            Assert.True(File.Exists(_filePath + "sample.dat"));
        }

        [Test]
        public void FailToSaveFile()
        {
            ITempToPermFileMover mover = new IOExceptionTempToPermFileMover();
            string expectedExceptionMessage = "Deliberate Exception";
            string actualExceptionMessage = null;

            try
            {
                mover.TryMoveFromTempToPerm(_filePath + "sample.dat1");
            }
            catch (IOException ex)
            {
                actualExceptionMessage = ex.Message;
            }

            Assert.AreEqual(expectedExceptionMessage, actualExceptionMessage);
            Assert.True(File.Exists(_filePath + "sample.dat"));
            Assert.False(File.Exists(_filePath + "sample.dat1"));
        }
    }
}
