using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.SqlServer.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Agilis.Infra.Data.SqlServer.Repositories.Produtos
{
    public class FeatureRepository : EntityFrameworkRepository<Feature>
    {
        private readonly AgilisDbContext _agilisDbContext;

        public FeatureRepository(AgilisDbContext agilisDbContext)
            : base(agilisDbContext)
        {
            _agilisDbContext = agilisDbContext;
        }

        public override Task AdicionarAsync(Feature feature)
        {
            _agilisDbContext.Entry(feature.Epico).State = EntityState.Unchanged;
            return base.AdicionarAsync(feature);
        }
    }
}