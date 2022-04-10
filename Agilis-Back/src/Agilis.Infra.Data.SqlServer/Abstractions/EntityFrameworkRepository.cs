using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Agilis.Infra.Data.SqlServer.Abstractions
{
    public abstract class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entidade
    {
        private readonly AgilisDbContext _agilisDbContext;

        public EntityFrameworkRepository(AgilisDbContext agilisDbContext)
        {
            _agilisDbContext = agilisDbContext;
        }

        public virtual async Task AdicionarAsync(TEntity entity) => await _agilisDbContext.Set<TEntity>().AddAsync(entity);

        public virtual Task AlterarAsync(TEntity entity)
        {
            _agilisDbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual IQueryable<TEntity> Consultar() => _agilisDbContext.Set<TEntity>().AsQueryable();

        public virtual Task<TEntity> ConsultarPorIdAsync(Guid id) => _agilisDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task ExcluirAsync(Guid id)
        {
            var entidade = await ConsultarPorIdAsync(id);
            _agilisDbContext.Set<TEntity>().Remove(entidade);
        }

        public virtual async Task ExcluirAsync(Func<TEntity, bool> predicate)
        {
            var entidades = Consultar().Where(predicate);
            _agilisDbContext.Set<TEntity>().RemoveRange(entidades);
        }
    }
}
