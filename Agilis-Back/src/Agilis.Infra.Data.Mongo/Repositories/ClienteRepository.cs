using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Mongo.Repositories
{
    public class ClienteRepository : MongoRepository<Cliente>, IRepository<Cliente>
    {
        public ClienteRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
