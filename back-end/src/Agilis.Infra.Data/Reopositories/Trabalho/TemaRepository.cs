using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Infra.Data.Reopositories.Trabalho
{
    public class TemaRepository : MongoRepository<Tema>, ITemaRepository
    {
        public TemaRepository(IMongoDatabase mongoDatabase, IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
        }

        public IEnumerable<Tema> ConsultarTodos(IEnumerable<Guid> produtosId)
        {
            return AsQueryable()
                   .Where(t => produtosId.Contains(t.ProdutoId))
                   .OrderBy(p => p.Nome)
                   .ToList();
        }
    }
}
