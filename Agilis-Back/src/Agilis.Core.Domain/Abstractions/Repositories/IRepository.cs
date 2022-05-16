using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task ExcluirAsync(Expression<Func<TEntity, bool>> expression);
        Task ExcluirAsync(IEnumerable<Guid> ids);
        Task ExcluirAsync(IEnumerable<TEntity> entities);
        Task ExcluirNotInAsync(IEnumerable<Guid> ids);
    }
}
