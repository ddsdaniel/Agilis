using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Core.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entidade
    {
        public Task<TEntity> ConsultarPorIdAsync(Guid id);
        public IQueryable<TEntity> Consultar();
        public Task AdicionarAsync(TEntity entity);
        public Task AlterarAsync(TEntity entity);
        public Task ExcluirAsync(Guid id);
        Task ExcluirAsync(Func<TEntity, bool> predicate);
    }
}
