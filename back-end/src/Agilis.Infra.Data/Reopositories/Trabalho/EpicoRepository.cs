using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Infra.Data.Reopositories.Trabalho
{
    public class EpicoRepository : MongoRepository<Epico>, IEpicoRepository
    {
        public EpicoRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }

        public IEnumerable<Epico> ConsultarTodos(IEnumerable<Guid> temasId)
        {
            return AsQueryable()
                   .Where(e => temasId.Contains(e.TemaId))
                   .OrderBy(e => e.Nome)
                   .ToList();
        }
    }
}
