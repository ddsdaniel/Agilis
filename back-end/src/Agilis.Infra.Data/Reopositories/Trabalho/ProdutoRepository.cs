using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Pessoas;
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
        private readonly ITimeRepository _timeRepository;

        public ProdutoRepository(IMongoDatabase mongoDatabase, 
                                 ITimeRepository timeRepository, 
                                 IClientSessionHandle session = null)
            : base(mongoDatabase, session)
        {
            _timeRepository = timeRepository;
        }

        public ICollection<Produto> ConsultarTodos(IUsuario usuario)
        {
            var timesId = ObterTimesDoUsuario(usuario).ToList();

            return AsQueryable()
                   .Where(p => timesId.Contains(p.TimeId))
                   .OrderBy(p => p.Nome)
                   .ToList();
        }

        public IQueryable<Guid> ObterTimesDoUsuario(IUsuario usuario)
        {
            return _timeRepository.ObterTimes(usuario).Select(t => t.Id);
        }
    }
}
