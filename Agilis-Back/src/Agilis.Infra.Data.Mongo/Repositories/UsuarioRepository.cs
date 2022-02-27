using MongoDB.Driver;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using Agilis.Infra.Seguranca.Models.Entities;

namespace Agilis.Infra.Data.Mongo.Repositories
{
    public class UsuarioRepository : MongoRepository<Usuario>, IRepository<Usuario>
    {
        public UsuarioRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
