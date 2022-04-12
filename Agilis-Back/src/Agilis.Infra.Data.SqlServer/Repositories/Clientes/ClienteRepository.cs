using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Clientes
{
    public class ClienteRepository : EntityFrameworkRepository<Cliente>
    {
        public ClienteRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}