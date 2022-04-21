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
            _agilisDbContext.Entry(tarefa.Feature).State = EntityState.Unchanged;
            _agilisDbContext.Entry(tarefa.Relator).State = EntityState.Unchanged;
            _agilisDbContext.Entry(tarefa.Solucionador).State = EntityState.Unchanged;
            return base.AdicionarAsync(tarefa);
        }

        public override IQueryable<Tarefa> Consultar()
        {
            return base.Consultar()
                .Include(t => t.Feature)
                .Include(t => t.Relator)
                .Include(t => t.Solucionador);
        }
    }
}