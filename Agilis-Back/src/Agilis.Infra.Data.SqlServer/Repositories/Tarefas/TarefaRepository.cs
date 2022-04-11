using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Tarefas
{
    public class TarefaRepository : EntityFrameworkRepository<Tarefa>
    {
        public TarefaRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}