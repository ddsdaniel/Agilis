using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Infra.Data.Reopositories.Trabalho
{
    public class UserStoryRepository : MongoRepository<UserStory>, IUserStoryRepository
    {
        public UserStoryRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }

        public IEnumerable<UserStory> ConsultarTodas(IEnumerable<Guid> epicosId)
        {
            return AsQueryable()
                   .Where(us => epicosId.Contains(us.EpicoId))
                   .OrderBy(us => us.Nome)
                   .ToList();
        }
    }
}
