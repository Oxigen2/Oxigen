using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using Oxigen.Core.Syndication;
using Oxigen.Core.QueryDtos.Syndication;

namespace Oxigen.Core.RepositoryInterfaces.Syndication
{
    public interface IRSSFeedRepository : IRepository<RSSFeed>
    {
        IList<RSSFeedDto> GetRSSFeedSummaries();
    }
}
