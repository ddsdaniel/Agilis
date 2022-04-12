using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Times
{
    public class TimeRepository : EntityFrameworkRepository<Time>
    {
        public TimeRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}