using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class RSSFeedRepository : Repository<RSSFeed>, IRSSFeedRepository
    {
        public IList<RSSFeedDto> GetRSSFeedSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetRSSFeedSummaries")
                .SetResultTransformer(Transformers.AliasToBean<RSSFeedDto>());

            return query.List<RSSFeedDto>();
        }
    }
}
