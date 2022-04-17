using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class EpicoRepository : EntityFrameworkRepository<Epico>
    {
        private readonly AgilisDbContext _agilisDbContext;

        public EpicoRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
            _agilisDbContext = agilisDbContext;
        }

        public override Task AdicionarAsync(Epico epico)
        {
            _agilisDbContext.Entry(epico.Produto).State = EntityState.Unchanged;
            return base.AdicionarAsync(epico);
        }

        public override IQueryable<Epico> Consultar()
        {
            return base.Consultar()
                .Include(e => e.Produto);
        }
    }
}