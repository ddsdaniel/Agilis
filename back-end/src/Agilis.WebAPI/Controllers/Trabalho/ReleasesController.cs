using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using DDS.WebAPI.Models.ViewModels;
using Agilis.Domain.Models.ForeignKeys;
using Agilis.Domain.Abstractions.Services.Trabalho;

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

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="releaseService">Instância do Automapper</param>
        public ReleasesController(IReleaseService releaseService) 
        {
            _releaseService = releaseService;
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
