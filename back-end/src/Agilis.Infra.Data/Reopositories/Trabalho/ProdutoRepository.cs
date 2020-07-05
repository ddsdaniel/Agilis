using Agilis.Domain.Abstractions.Repositories.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Infra.Data.Mongo.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Infra.Data.Reopositories.Trabalho
{
    public class ProdutoRepository : MongoRepository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(IMongoDatabase mongoDatabase, 
                                 IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
            
        }

        public IEnumerable<Produto> ConsultarTodos(IEnumerable<Guid> timesId)
        {
            return AsQueryable()
                   .Where(p => timesId.Contains(p.TimeId))
                   .OrderBy(p => p.Nome)
                   .ToList();
        }
    }
}
