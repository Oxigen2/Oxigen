using NUnit.Framework;
using Rhino.Mocks;
using OxigenIIAdvertising.ContentExchanger;

namespace Oxigen.Tests32
{
    [TestFixture]
    public class FailedInternetConnectionAttemptRegistryAccessorTests
    {
        [Test]
        public void CanRecordFailedInternetConnectionAttempt()
        {
            // Arrange
            IFailedInternetConnectionAttemptAccessor failedInternetConnectionAttemptRegistryAccessor =
                MockRepository.GenerateMock<IFailedInternetConnectionAttemptAccessor>();
            failedInternetConnectionAttemptRegistryAccessor.Expect(x => x.GetFailedAttempts()).Return(24);

            int count = 0;

            // Act
            for (int i = 0; i < 24; i ++)
            {
                failedInternetConnectionAttemptRegistryAccessor.RecordFailedAttempt();
                count++;
            }

            // Assert
            Assert.AreEqual(count, failedInternetConnectionAttemptRegistryAccessor.GetFailedAttempts());
        }
    }
}
