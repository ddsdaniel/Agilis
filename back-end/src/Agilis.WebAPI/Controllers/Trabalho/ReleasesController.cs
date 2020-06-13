using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ReleasesController : CrudController<ReleaseViewModel, ReleaseViewModel, Release>
    {
        private readonly IReleaseService _releaseService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="usuarioLogado">Instância do usuário logado</param>
        /// <param name="mapper">Instância do Automapper</param>
        public ReleasesController(IReleaseService service,
                                 IUsuario usuarioLogado,
                                 IMapper mapper) 
            : base(service, mapper)
        {
            _releaseService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa")]
        [ProducesResponseType(typeof(ICollection<ReleaseViewModel>), StatusCodes.Status200OK)]
        public override ActionResult<ICollection<ReleaseViewModel>> Pesquisar([FromQuery] string filtro)
        {
            var lista = _releaseService.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<ReleaseViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Consulta todos os releases do usuário logado
        /// </summary>
        /// <returns>Retorna todas as releases do usuário logado</returns>
        public override ActionResult<ICollection<ReleaseViewModel>> ConsultarTodos()
        {

            var lista = _releaseService.ConsultarTodos(_usuarioLogado).OrderBy(p => p.Nome);

            var listaViewModel = _mapper.Map<List<ReleaseViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<ReleaseViewModel> Ordenar(ICollection<ReleaseViewModel> lista)
                => lista.OrderBy(p => p.Nome)
                        .ToList();

        /// <summary>
        /// Adiciona um sprint ao release
        /// </summary>
        /// <param name="autoMapper"></param>
        /// <param name="releaseId"></param>
        /// <param name="sprintViewModel"></param>
        /// <returns></returns>
        [HttpPost("{releaseId:guid}/sprints")]
        [ProducesResponseType(typeof(ICollection<SprintBasicViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarSprint([FromServices] IMapper autoMapper,
                                                        Guid releaseId,
                                                        SprintViewModel sprintViewModel)
        {

            var sprint = autoMapper.Map<Sprint>(sprintViewModel);

            var sprintVO = await _releaseService.AdicionarSprint(releaseId, sprint);
            if (_releaseService.Invalid)
                return BadRequest(_releaseService.Notifications);

            var sprintBasicViewModel = autoMapper.Map<SprintBasicViewModel>(sprintVO);

            return Ok(sprintBasicViewModel);
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
