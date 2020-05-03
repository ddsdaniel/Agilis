using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Reopositories.Pessoas
{
    public class AtorRepository : MongoRepository<Ator>, IAtorRepository
    {
        public AtorRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }
    }
}
