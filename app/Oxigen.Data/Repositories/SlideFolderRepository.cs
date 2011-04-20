using System;
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
    public class SlideFolderRepository : Repository<SlideFolder>, ISlideFolderRepository
    {
        public IList<SlideFolderDto> GetSlideFolderSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetSlideFolderSummaries")
                .SetResultTransformer(Transformers.AliasToBean<SlideFolderDto>());

            return query.List<SlideFolderDto>();
        }

        public IList<SlideFolderDto> GetByPublisher(int publisherId)
        {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetSlideFolderSummariesByPublisher")
                .SetParameter("publisherId", publisherId)
                .SetResultTransformer(Transformers.AliasToBean<SlideFolderDto>());

            return query.List<SlideFolderDto>();
        }

        public IList<SlideFolder> GetSlideFoldersWithTooManySlides()
        {
            ISession session = NHibernateSession.Current;
            var criteria = session.CreateCriteria(typeof(SlideFolder))
                .Add(Restrictions.GtProperty("SlideCount", "MaxSlideCount"))
                .Add(Restrictions.Gt("MaxSlideCount", 0));
            return criteria.List<SlideFolder>();
        }
    }
}
