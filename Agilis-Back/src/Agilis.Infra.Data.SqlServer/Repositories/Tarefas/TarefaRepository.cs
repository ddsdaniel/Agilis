using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Agilis.Infra.Data.SqlServer.Repositories.Tarefas
{
    public class TarefaRepository : EntityFrameworkRepository<Tarefa>
    {
        private readonly AgilisDbContext _agilisDbContext;

        public TarefaRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
            _agilisDbContext = agilisDbContext;
        }

        public override Task AdicionarAsync(Tarefa tarefa)
        {
            _agilisDbContext.Entry(tarefa.Produto).State = EntityState.Unchanged;
            return base.AdicionarAsync(tarefa);
        }
    }
}