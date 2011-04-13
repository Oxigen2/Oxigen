using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public IList<ChannelDto> GetChannelSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetChannelSummaries")
                .SetResultTransformer(Transformers.AliasToBean<ChannelDto>());

            return query.List<ChannelDto>();
        }
    }
}
