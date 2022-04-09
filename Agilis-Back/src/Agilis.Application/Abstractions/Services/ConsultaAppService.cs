using AutoMapper;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Abstractions.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Abstractions.Services
{
    public abstract class ConsultaAppService<TViewModelConsulta, TEntity> : AppService
        where TViewModelConsulta : class
        where TEntity : Entidade
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;

        protected ConsultaAppService(IMapper mapper,
                                     IRepository<TEntity> repository
            ) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual TViewModelConsulta[] ConsultarTodos()
        {
            var lista = _repository.Consultar().ToArray();

            var viewModels = _mapper.Map<TViewModelConsulta[]>(lista);

            return viewModels;
        }

        public virtual async Task<TViewModelConsulta> ConsultarPorIdAsync(Guid id)
        {
            var entidade = await _repository.ConsultarPorIdAsync(id);

            var viewModel = _mapper.Map<TViewModelConsulta>(entidade);

            return viewModel;
        }
    }
}
