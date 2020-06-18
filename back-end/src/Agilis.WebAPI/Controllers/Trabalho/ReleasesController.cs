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

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ReleasesController : GenericController
    {
        private readonly IReleaseService _releaseService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="releaseService">Instância do Automapper</param>
        public ReleasesController(IReleaseService releaseService,
                                  IMapper mapper) 
        {
            _releaseService = releaseService;
            _mapper = mapper;
        }

        /// <summary>
        /// Consulta uma release no repositório
        /// </summary>
        /// <param name="id">Id da release que está sendo consultada</param>
        /// <returns>View model da release</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ReleaseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<ReleaseViewModel>> ConsultarPorId(Guid id)
        {
            var release = await _releaseService.ConsultarPorId(id);

            if (release == null)
                return CustomNotFound(nameof(id), "Release não encontrada");

            var releaseViewModel = _mapper.Map<ReleaseViewModel>(release);

            return Ok(releaseViewModel);
        }

        /// <summary>
        /// Renomeia a release
        /// </summary>
        /// <param name="timeId">Id do time</param>
        /// <param name="releaseId">Id da release</param>
        /// <param name="stringContainerViewModel">Novo nome da release</param>
        /// <returns>Status200OK</returns>
        [HttpPatch("{timeId:guid}/{releaseId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Renomear(Guid timeId,
                                                 Guid releaseId,
                                                 StringContainerViewModel stringContainerViewModel)
        {
            await _releaseService.Renomear(timeId, releaseId, stringContainerViewModel.Texto);
            if (_releaseService.Invalid)
                return BadRequest(_releaseService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona um sprint ao release
        /// </summary>
        /// <param name="releaseId">Id da release em que o sprint será adicionado</param>
        /// <param name="sprintViewModel">Nome da sprint a ser adicionada</param>
        /// <returns>Id e nome da sprint adicionada</returns>
        [HttpPost("{releaseId:guid}/sprints")]
        [ProducesResponseType(typeof(SprintFK), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarSprint(Guid releaseId,
                                                        StringContainerViewModel sprintViewModel)
        {
            var sprintFK = await _releaseService.AdicionarSprint(releaseId, sprintViewModel.Texto);
            if (_releaseService.Invalid)
                return BadRequest(_releaseService.Notifications);

            return Ok(sprintFK);
        }

        /// <summary>
        /// Remove um sprint do release+
        /// </summary>
        /// <param name="releaseId"></param>
        /// <param name="sprintId"></param>
        /// <returns></returns>
        [HttpDelete("{releaseId:guid}/sprints/{sprintId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirSprint(Guid releaseId, 
                                                      Guid sprintId)
        {
            await _releaseService.ExcluirSprint(releaseId, sprintId);
            if (_releaseService.Invalid)
                return BadRequest(_releaseService.Notifications);

            return Ok();
        }
    }
}
