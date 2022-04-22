using Agilis.Core.Domain.Models.Entities.Tarefas;
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
            _agilisDbContext.Entry(tarefa.Feature).State = EntityState.Unchanged;
            _agilisDbContext.Entry(tarefa.Relator).State = EntityState.Unchanged;
            _agilisDbContext.Entry(tarefa.Solucionador).State = EntityState.Unchanged;
            _agilisDbContext.Entry(tarefa.Tags).State = EntityState.Unchanged;
            return base.AdicionarAsync(tarefa);
        }

        public override IQueryable<Tarefa> Consultar()
        {
            return base.Consultar()
                .Include(t => t.Feature).ThenInclude(f => f.Epico).ThenInclude(e => e.Produto)
                .Include(t => t.Relator)
                .Include(t => t.Solucionador);
        }
    }
}