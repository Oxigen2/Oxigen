using System;
using Oxigen.Core;

namespace Tests.Oxigen.Core
{
    public class PublisherInstanceFactory
    {
        public static Publisher CreateValidTransientPublisher() {
            return new Publisher() {
			    UserID = 3, 
				FirstName = "John", 
				LastName = "John", 
				DisplayName = "John", 
				EmailAddress = "John", 
				UsedBytes = 100, 
				TotalAvailableBytes = 10000 
            };
        }
    }
}
