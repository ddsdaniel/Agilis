using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using Domain = Agilis.Core.Domain.Models.Entities.Tarefas;

namespace Agilis.Infra.Data.Mongo.Repositories.Tarefas
{
    public class TagRepository : MongoRepository<Domain.Tag>, IRepository<Domain.Tag>
    {
        public TagRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session) : base(mongoDatabase, session)
        {
        }
    }
}
