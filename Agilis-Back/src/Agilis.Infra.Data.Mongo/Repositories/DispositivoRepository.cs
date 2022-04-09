using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;

namespace Agilis.Infra.Data.Mongo.Repositories
{
    public class DispositivoRepository : MongoRepository<Dispositivo>, IRepository<Dispositivo>
    {
        public DispositivoRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
