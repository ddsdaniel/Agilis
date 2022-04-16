using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class EpicoRepository : EntityFrameworkRepository<Epico>
    {
        public EpicoRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}