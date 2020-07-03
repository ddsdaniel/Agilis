using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Reopositories.Trabalho
{
    public class TemaRepository : MongoRepository<Tema>, ITemaRepository
    {
        public TemaRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }
    }
}
