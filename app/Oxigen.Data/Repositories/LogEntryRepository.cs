using Oxigen.Core.Logger;
using Oxigen.Core.RepositoryInterfaces;
using SharpArch.Data.NHibernate;

namespace Oxigen.Data.Repositories
{
    public class LogEntryRepository : Repository<LogEntry>, ILogEntryRepository
    {
    }
}
