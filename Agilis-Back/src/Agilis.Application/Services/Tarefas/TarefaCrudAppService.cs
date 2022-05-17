using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using MediatR;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Agilis.Application.Services.Tarefas
{
    public class TarefaCrudAppService
        : CrudAppService<TarefaViewModel, TarefaViewModel, Tarefa>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TarefaCrudAppService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Tarefa>(), unitOfWork, mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string[] ConsultarTags()
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            var tags = tarefaRepository
                .Consultar()
                .SelectMany(t => t.Tags)
                .OrderBy(t => t.Nome)
                .Select(t => t.Nome)
                .ToArray();

            tags = tags.Distinct().ToArray();

            return tags;
        }

        public IEnumerable<TarefaViewModel> Pesquisar(
            string sprintId, 
            string relatorId,
            string solucionadorId,
            string clienteId
            )
        {
            var tarefaRepository = _unitOfWork.ObterRepository<Tarefa>();

            var query = tarefaRepository.Consultar();

            //if (tipoRepeticao.HasValue)
            //    query = query.Where(t => t.Repeticao.Tipo == tipoRepeticao.Value);

            if (!String.IsNullOrEmpty(sprintId))
                query = query.Where(t => t.Sprint.Id == new Guid(sprintId));

            if (!String.IsNullOrEmpty(relatorId))
                query = query.Where(t => t.Relator.Id == new Guid(relatorId));

            if (!String.IsNullOrEmpty(solucionadorId))
                query = query.Where(t => t.Solucionador.Id == new Guid(solucionadorId));

            if (!String.IsNullOrEmpty(clienteId))
                query = query.Where(t => t.Cliente.Id == new Guid(clienteId));            

            var queryOrdenada = query
                .Take(300)
                .OrderByDescending(t => t.Valor);

            var tarefas = queryOrdenada.ToArray();

            return _mapper.Map<IEnumerable<TarefaViewModel>>(tarefas);
        }
    }
}
