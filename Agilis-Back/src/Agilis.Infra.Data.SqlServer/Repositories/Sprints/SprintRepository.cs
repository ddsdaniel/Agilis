using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Sprints
{
    public class SprintRepository : EntityFrameworkRepository<Sprint>
    {
        public SprintRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}