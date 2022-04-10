using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Seguranca
{
    public class UsuarioRepository : EntityFrameworkRepository<Usuario>
    {
        public UsuarioRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}