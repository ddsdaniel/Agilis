using DDS.Domain.Core.Abstractions.Models.Entities;
using DDS.Domain.Core.Abstractions.Repositories;
using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Services
{
    /// <summary>
    /// Classe abstrata com métodos e dados comuns a todos os CRUDs
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class CrudService<TEntity> : Service, ICrudService<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Unidade de trabalho, usada para garantir a atomicidade
        /// </summary>
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly IRepository<TEntity> _repository;

        /// <summary>
        /// Construtor completo
        /// </summary>
        /// <param name="unitOfWork">Unidade de trabalho, passada por injeção de dependência</param>
        /// <param name="repository">Repositório da entidade que está sendo manipulada pelo CrudService. Detalhe: repositório que é a propriedade do Unit of Work</param>
        protected CrudService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        /// <summary>
        /// Adiciona uma entidade ao repositório, se ela estiver válida
        /// </summary>
        /// <param name="entity">Entidade que será adicionada</param>
        /// <returns>Task correspondente à adição</returns>
        public virtual async Task Adicionar(TEntity entity)
        {
            if (!entity.Valid)
                AddNotifications(entity);
            else
                await _repository.Adicionar(entity);
        }

        /// <summary>
        /// Atualiza/altera uma entidade no repositório, se ela estiver válida
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada</param>
        /// <returns>Task correspondente à atualização</returns>
        public virtual async Task Atualizar(TEntity entity)
        {
            if (!entity.Valid)
                AddNotifications(entity);
            else
                await _repository.Atualizar(entity);
        }

        /// <summary>
        /// Realiza o commit no unit of work
        /// </summary>
        /// <returns></returns>
        public async Task Commit() => await _unitOfWork.Commit();

        public virtual IQueryable<TEntity> Consultar()
            => _repository.AsQueryable();

        /// <summary>
        /// Recupera uma entidade do repositório a partir do seu id
        /// </summary>
        /// <param name="id">Id da entidade a ser recuperada</param>
        /// <returns>Task correspondente à entidade consultada, também pode retorna null, caso a entidade não seja encontrada</returns>
        public virtual async Task<TEntity> ConsultarPorId(Guid id) => await _repository.ConsultarPorId(id);

        /// <summary>
        /// Retorna todas as entidades desse repositório
        /// </summary>
        /// <returns>Lista de todas as entidades desse repositório</returns>
        public virtual IEnumerable<TEntity> ConsultarTodos() => _repository.AsQueryable().ToList();

        /// <summary>
        /// Exclui a entidade do repositório
        /// </summary>
        /// <param name="id">Id da entidade a ser excluída</param>
        /// <returns>Task correspondente à exclusão</returns>
        public virtual async Task Excluir(Guid id) => await _repository.Excluir(id);
        public abstract IEnumerable<TEntity> Pesquisar(string filtro);
    }
}
