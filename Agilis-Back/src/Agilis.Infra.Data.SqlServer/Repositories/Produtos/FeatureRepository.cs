using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class FeatureRepository : EntityFrameworkRepository<Feature>
    {
        public FeatureRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
        }
    }
}