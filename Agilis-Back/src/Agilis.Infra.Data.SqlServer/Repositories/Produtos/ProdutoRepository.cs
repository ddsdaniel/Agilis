using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class ProdutoRepository : EntityFrameworkRepository<Produto>
    {
        public ProdutoRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }

        public override IQueryable<Produto> Consultar()
        {
            return base.Consultar()
                .Include(p => p.Epicos).ThenInclude(e => e.Features).ThenInclude(f => f.Tarefas);
        }
    }
}