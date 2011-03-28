using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        public IList<SlideDto> GetSlideSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetSlideSummaries")
                .SetResultTransformer(Transformers.AliasToBean<SlideDto>());

            return query.List<SlideDto>();
        }
    }
}
