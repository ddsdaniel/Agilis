using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Agilis.Core.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : Entidade
    {
        public bool ConsultarSeExiste(Guid id);
        public Task<TEntity> ConsultarPorIdAsync(Guid id);
        public IQueryable<TEntity> Consultar();
        
        public Task AdicionarAsync(TEntity entity);
        public Task AdicionarAsync(IEnumerable<TEntity> lista);
        
        public Task AlterarAsync(TEntity entity);
        public Task AlterarAsync<TField>(Expression<Func<TEntity, TField>> field, TField value, Expression<Func<TEntity, bool>> expression);

        public Task ExcluirAsync(Expression<Func<TEntity, bool>> expression);
        public Task ExcluirAsync(Guid id);
        public Task ExcluirVariosAsync(IEnumerable<TEntity> lista);

        public void ExcluirCampo(string nome);
    }
}
