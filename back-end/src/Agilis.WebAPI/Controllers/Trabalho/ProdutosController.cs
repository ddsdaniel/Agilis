using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using DDS.WebAPI.Models.ViewModels;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Flunt.Notifications;
using System.Collections.Generic;
using Agilis.WebAPI.ViewModels.Trabalho;
using AutoMapper;
using Agilis.Domain.Models.ValueObjects.Trabalho;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : GenericController
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="produtoService">Instância do Automapper</param>
        public ProdutosController(IProdutoService produtoService,
                                  IMapper mapper) 
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Consulta uma produto no repositório
        /// </summary>
        /// <param name="id">Id da produto que está sendo consultada</param>
        /// <returns>View model da produto</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<ProdutoViewModel>> ConsultarPorId(Guid id)
        {
            var produto = await _produtoService.ConsultarPorId(id);

            if (produto == null)
                return CustomNotFound(nameof(id), "Produto não encontrada");

            var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoViewModel);
        }

        /// <summary>
        /// Renomeia a produto
        /// </summary>
        /// <param name="timeId">Id do time</param>
        /// <param name="produtoId">Id da produto</param>
        /// <param name="stringContainerViewModel">Novo nome da produto</param>
        /// <returns>Status200OK</returns>
        [HttpPatch("{timeId:guid}/{produtoId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Renomear(Guid timeId,
                                                 Guid produtoId,
                                                 StringContainerViewModel stringContainerViewModel)
        {
            await _produtoService.Renomear(timeId, produtoId, stringContainerViewModel.Texto);
            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona uma jornada ao produto
        /// </summary>
        /// <param name="produtoId">Id do produto em que a jornada será adicionada</param>
        /// <param name="jornadaViewModel">Nome da jornada a ser adicionada</param>
        /// <returns>Id e nome da jornada adicionada</returns>
        [HttpPost("{produtoId:guid}/jornadas")]
        [ProducesResponseType(typeof(Jornada), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarJornada(Guid produtoId,
                                                        StringContainerViewModel jornadaViewModel)
        {
            var jornada = await _produtoService.AdicionarJornada(produtoId, jornadaViewModel.Texto);
            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            return Ok(jornada);
        }

        /// <summary>
        /// Remove uma jornada do produto
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="jornadaId"></param>
        /// <returns></returns>
        [HttpDelete("{produtoId:guid}/jornadas/{jornadaId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirJornada(Guid produtoId, 
                                                       Guid jornadaId)
        {
            await _produtoService.ExcluirJornada(produtoId, jornadaId);
            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            return Ok();
        }
    }
}
