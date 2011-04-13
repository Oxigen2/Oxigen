using SharpArch.Core.PersistenceSupport;
using System.Collections.Generic;
using Oxigen.Core;
using Oxigen.Core.QueryDtos;

namespace Oxigen.Core.RepositoryInterfaces
{
    public interface ISlideFolderRepository : IRepository<SlideFolder>
    {
        IList<SlideFolderDto> GetSlideFolderSummaries();
        IList<SlideFolderDto> GetByPublisher(int publisherId);
    }
}
