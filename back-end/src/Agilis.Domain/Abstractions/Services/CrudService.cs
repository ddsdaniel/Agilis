using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Abstractions.Repositories;
using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Services
{
    public abstract class CrudService<TEntity> : Service, ICrudService<TEntity>
        where TEntity : Entity
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;

        public CrudService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            if (!entity.Valid)
                AddNotifications(entity);
            else
                await _repository.Adicionar(entity);
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            if (!entity.Valid)
                AddNotifications(entity);
            else
                await _repository.Atualizar(entity);
        }

        public async Task Commit() => await _unitOfWork.Commit();

        public async Task<TEntity> ConsultarPorId(Guid id) => await _repository.ConsultarPorId(id);

        public ICollection<TEntity> ConsultarTodos() => _repository.AsQueryable().ToList();

        public async Task Excluir(Guid id) => await _repository.Excluir(id);
    }
}
