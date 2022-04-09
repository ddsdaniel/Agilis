using AutoMapper;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using System;
using System.Threading.Tasks;
using Agilis.Core.Domain.Events;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;

namespace Agilis.Application.Abstractions.Services
{
    public abstract class CrudAppService<
        TViewModelCadastro,
        TViewModelConsulta,
        TEntity
        > : ConsultaAppService<TViewModelConsulta, TEntity>
        where TViewModelCadastro : class
        where TViewModelConsulta : class
        where TEntity : Entidade        
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected CrudAppService(IMapper mapper,
                                 IRepository<TEntity> repository,
                                 IUnitOfWork unitOfWork) 
            : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<TViewModelConsulta> AdicionarAsync(TViewModelCadastro novaEntidadeViewModel)
        {
            var entidade = _mapper.Map<TEntity>(novaEntidadeViewModel);

            if (entidade.Invalido)
            {
                ImportarCriticas(entidade);
                return null;
            }
            else
            {
                await _repository.AdicionarAsync(entidade);

                var entidadeAdicionadaEvent = (EntidadeAdicionadaDomainEvent<TEntity>)Activator.
                    CreateInstance(typeof(EntidadeAdicionadaDomainEvent<TEntity>), new object[] { entidade });

                await _unitOfWork.CommitAsync();

                var viewModelConsulta = _mapper.Map<TViewModelConsulta>(entidade);
                return viewModelConsulta;
            }
        }

        public virtual async Task AlterarAsync(Guid id, TViewModelCadastro viewModelCadastro)
        {
            var depois = _mapper.Map<TEntity>(viewModelCadastro);

            if (depois.Id != id)
            {
                Criticar("Ids não conferem");
            }
            else if (depois.Invalido)
            {
                ImportarCriticas(depois);
            }
            else
            {
                var antes = await _repository.ConsultarPorIdAsync(id);

                await _repository.AlterarAsync(depois);

                var entidadeAlteradaEvent = (EntidadeAlteradaDomainEvent<TEntity>)Activator.
                    CreateInstance(typeof(EntidadeAlteradaDomainEvent<TEntity>), new object[] { antes, depois });

                if (Valido)
                {
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        public virtual async Task ExcluirAsync(Guid id)
        {
            var entidade = await _repository.ConsultarPorIdAsync(id);

            if (entidade == null)
            {
                Criticar("Registro não encontrado");
            }
            else
            {
                await _repository.ExcluirAsync(id);

                var entidadeExcluidaEvent = (EntidadeExcluidaDomainEvent<TEntity>)Activator.
                CreateInstance(typeof(EntidadeExcluidaDomainEvent<TEntity>), new object[] { entidade });

                if (Valido)
                {
                    await _unitOfWork.CommitAsync();
                }
            }
        }
    }
}
