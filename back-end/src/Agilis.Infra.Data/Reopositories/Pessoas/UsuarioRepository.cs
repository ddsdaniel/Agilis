using DDS.Domain.Core.Model.ValueObjects;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System.Linq;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Pessoas;

namespace Agilis.Infra.Data.Reopositories.Pessoas
{
    public class UsuarioRepository : MongoRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null) 
            : base(mongoDatabase, session)
        {
        }

        public Usuario ConsultarPorEmail(Email email)
        {
            return AsQueryable()
                .Where(u => u.Email.Endereco == email.Endereco)
                .FirstOrDefault();
        }
    }
}
