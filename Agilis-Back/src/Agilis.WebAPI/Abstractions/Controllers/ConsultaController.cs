using Agilis.Core.Domain.Abstractions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Agilis.Application.Abstractions.Services;

namespace Agilis.WebAPI.Abstractions.Controllers
{
    public abstract class ConsultaController<TViewModelConsulta,TEntity> : AgilisController
        where TEntity : Entidade
        where TViewModelConsulta : class
    {
        private readonly ConsultaAppService<TViewModelConsulta, TEntity> _consultaAppService;

        public ConsultaController(ConsultaAppService<TViewModelConsulta, TEntity> consultaAppService)
        {
            _consultaAppService = consultaAppService;
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TViewModelConsulta>> ConsultarPorId(Guid id)
        {
            var viewModel = await _consultaAppService.ConsultarPorIdAsync(id);

            if (viewModel == null)
                return CustomNotFound(nameof(id), "Registro não encontrado");

            return Ok(viewModel);
        }
    }
}
