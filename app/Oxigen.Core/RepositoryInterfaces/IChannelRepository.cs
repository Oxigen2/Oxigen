using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;

namespace Oxigen.Core.RepositoryInterfaces
{
    public interface IChannelRepository : IRepository<Channel>
    {
        IList<ChannelDto> GetChannelSummaries();
        IList<ChannelDto> GetByPublisher(int publisherId);
    }
}
