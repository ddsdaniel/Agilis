using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System.Linq;

namespace Agilis.Infra.Data.Reopositories.Pessoas
{
    public class TimeRepository : MongoRepository<Time>, ITimeRepository
    {
        public TimeRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }

        public IQueryable<Time> ObterTimes(IUsuario usuario)
        {
            return AsQueryable()
                .Where(t => t.Administradores.Any(a => a.Id == usuario.Id) ||
                            t.Colaboradores.Any(a => a.Id == usuario.Id)
                );
        }
    }
}
