using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class TemplateRepository : Repository<Template>, ITemplateRepository
    {
        public IList<TemplateDto> GetTemplateSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetTemplateSummaries")
                .SetResultTransformer(Transformers.AliasToBean<TemplateDto>());

            return query.List<TemplateDto>();
        }
    }
}
