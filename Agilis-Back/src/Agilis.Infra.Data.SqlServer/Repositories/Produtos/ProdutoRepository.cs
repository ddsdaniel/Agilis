using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class ProdutoRepository : EntityFrameworkRepository<Produto>
    {
        public ProdutoRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}