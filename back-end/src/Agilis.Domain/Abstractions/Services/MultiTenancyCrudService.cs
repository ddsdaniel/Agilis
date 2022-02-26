using Agilis.Domain.Abstractions.Entities;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using DDS.Domain.Core.Abstractions.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Abstractions.Services
{
    public abstract class MultiTenancyCrudService<TEntity> : CrudService<TEntity>
        where TEntity : MultiTenancyEntity
    {
        protected MultiTenancyCrudService(IUnitOfWork unitOfWork, IRepository<TEntity> repository) 
            : base(unitOfWork, repository)
        {
        }

        public IEnumerable<TEntity> ConsultarTodos(IUsuario usuario)
            => _repository
                .AsQueryable()
                .Where(p => p.UsuarioId == usuario.Id)
                .ToList();
    }
}
