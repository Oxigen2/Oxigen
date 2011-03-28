using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;

namespace Oxigen.Core.RepositoryInterfaces
{
    public interface ITemplateRepository : IRepository<Template>
    {
        IList<TemplateDto> GetTemplateSummaries();
    }
}
