using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Seguranca
{
    public class RefreshTokenRepository : EntityFrameworkRepository<RefreshToken>
    {
        public RefreshTokenRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}