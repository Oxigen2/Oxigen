using System;
using NUnit.Framework;
using Oxigen.DurationDetectors;

namespace Tests.Oxigen.Web
{
    [TestFixture, Category("IntegrationTest")]
    public class MovieDurationTests
    {
        private double TIME_THRESHOLD_SECONDS = 1;

        [Test]
        public void CanGetCorrectQuicktimeDuration1()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 85.5d;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"C:\Oxigen2\Oxigen\tests\Oxigen.Tests\FileSystemResources\MovieDurationTests\MOV\sample.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        [Test]
        public void CanGetCorrectQuicktimeDuration2()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 45.07d;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"C:\Oxigen2\Oxigen\tests\Oxigen.Tests\FileSystemResources\MovieDurationTests\MOV\MonteCarloSundayHotshot.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        [Test]
        public void CanGetCorrectQuicktimeDuration3()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 38d;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"C:\Oxigen2\Oxigen\tests\Oxigen.Tests\FileSystemResources\MovieDurationTests\MOV\Madrid_20110505_Hotshot.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        [Test]
        public void CanGetCorrectWMPDuration1()
        {
            // Arrange
            WindowsMediaFormatsFileDurationDetector wmvDetector = new WindowsMediaFormatsFileDurationDetector();

            double actualDuration;
            double expectedDuration = 150.7d;

            // Act
            actualDuration = wmvDetector.GetDurationInSeconds(@"C:\Oxigen2\Oxigen\tests\Oxigen.Tests\FileSystemResources\MovieDurationTests\WMV\sample1.wmv");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        private void AssertWithThreshold(double expectedDuration, double actualDuration, double timeThreshold)
        {
            if (Math.Abs(expectedDuration - actualDuration) <= timeThreshold)
                Assert.Pass("Deviation between actual and expected values is within the threshold of " + timeThreshold+ " seconds. Expected value was " + expectedDuration + ", actual is " + actualDuration);

            Assert.Fail("Deviation between between actual and expected values is outside the threshold of " + timeThreshold + " seconds. Expected value was " + expectedDuration + ", actual is " + actualDuration);
        }
    }
}
