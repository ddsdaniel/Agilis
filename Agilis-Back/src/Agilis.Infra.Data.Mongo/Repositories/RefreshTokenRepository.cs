using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using Agilis.Infra.Seguranca.Models.Entities;

namespace Agilis.Infra.Data.Mongo.Repositories
{
    public class RefreshTokenRepository : MongoRepository<RefreshToken>, IRepository<RefreshToken>
    {
        public RefreshTokenRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
