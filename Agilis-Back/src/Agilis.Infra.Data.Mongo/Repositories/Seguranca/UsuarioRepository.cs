using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;

namespace Agilis.Infra.Data.Mongo.Repositories.Seguranca
{
    public class UsuarioRepository : MongoRepository<Usuario>, IRepository<Usuario>
    {
        public UsuarioRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
