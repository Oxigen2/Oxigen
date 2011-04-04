using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public IList<PublisherDto> GetPublisherSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetPublisherSummaries")
                .SetResultTransformer(Transformers.AliasToBean<PublisherDto>());

            return query.List<PublisherDto>();
        }


        public Publisher GetByUserId(int userId)
        {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            return session.CreateCriteria<Publisher>().Add(Expression.Eq("UserID", userId)).UniqueResult<Publisher>();

        }
    }
}
