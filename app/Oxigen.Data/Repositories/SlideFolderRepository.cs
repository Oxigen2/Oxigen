using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class SlideFolderRepository : Repository<SlideFolder>, ISlideFolderRepository
    {
        public IList<SlideFolderDto> GetSlideFolderSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetSlideFolderSummaries")
                .SetResultTransformer(Transformers.AliasToBean<SlideFolderDto>());

            return query.List<SlideFolderDto>();
        }
    }
}
