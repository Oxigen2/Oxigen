using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;

namespace Oxigen.Core.RepositoryInterfaces
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        IList<PublisherDto> GetPublisherSummaries();

        Publisher GetByUserId(int userId);
    }
}
