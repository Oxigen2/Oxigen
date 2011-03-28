using SharpArch.Data.NHibernate;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;
using Oxigen.Core.RepositoryInterfaces;

namespace Oxigen.Data.Repositories
{
    public class AssetContentRepository : Repository<AssetContent>, IAssetContentRepository
    {
        public IList<AssetContentDto> GetAssetContentSummaries() {
            ISession session = SharpArch.Data.NHibernate.NHibernateSession.Current;

            IQuery query = session.GetNamedQuery("GetAssetContentSummaries")
                .SetResultTransformer(Transformers.AliasToBean<AssetContentDto>());

            return query.List<AssetContentDto>();
        }
    }
}
