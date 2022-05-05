using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Models.ValueObjects.Produtos;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Mongo.Repositories
{
    public class FeatureRepository : MongoRepository<Feature>, IRepository<Feature>
    {
        public FeatureRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
