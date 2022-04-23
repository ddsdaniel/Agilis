using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Tarefas
{
    public class TagRepository : EntityFrameworkRepository<Tag>
    {
        public TagRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}