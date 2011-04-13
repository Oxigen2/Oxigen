using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class ChannelsSlideRepository : Repository<ChannelsSlide>, IChannelsSlideRepository
    {
        public IList<ChannelsSlideDto> GetChannelsSlideSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetChannelsSlideSummaries")
                .SetResultTransformer(Transformers.AliasToBean<ChannelsSlideDto>());

            return query.List<ChannelsSlideDto>();
        }
    }
}
