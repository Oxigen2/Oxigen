using System;
using NUnit.Framework;
using Oxigen.DurationDetectors;

namespace Tests.Oxigen.Web
{
    [TestFixture, Category("IntegrationTest")]
    public class MovieDurationTests
    {
        private double TIME_THRESHOLD_SECONDS = 0.5;

        [Test]
        public void CanGetCorrectQuicktimeDuration1()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 85.5;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"../../FileSystemResources/sample.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        [Test]
        public void CanGetCorrectQuicktimeDuration2()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 45.07;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"../../FileSystemResources/MonteCarloSundayHotshot.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        [Test]
        public void CanGetCorrectQuicktimeDuration3()
        {
            // Arrange
            QuicktimeFileDurationDetector qtDetector = new QuicktimeFileDurationDetector();
            double actualDuration;
            double expectedDuration = 38;

            // Act
            actualDuration = qtDetector.GetDurationInSeconds(@"../../FileSystemResources/Madrid_20110505_Hotshot.mov");

            // Assert
            AssertWithThreshold(expectedDuration, actualDuration, TIME_THRESHOLD_SECONDS);
        }

        private void AssertWithThreshold(double expectedDuration, double actualDuration, double timeThreshold)
        {
            if (Math.Abs(expectedDuration - actualDuration) <= timeThreshold)
                Assert.Pass("Difference between actual and expected values is within the threshold of " + timeThreshold);

            Assert.Fail("Difference between actual and expected values is outside the threshold of " + timeThreshold);
        }
    }
}
