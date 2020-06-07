using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Abstractions.Services.Pessoas;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Threading.Tasks;
using System;
using Agilis.WebAPI.ViewModels;
using DDS.Domain.Core.Model.ValueObjects;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class TimesController : CrudController<TimeViewModel, TimeViewModel, Time>
    {
        private readonly ITimeService _service;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public TimesController(ITimeService service, 
                              IMapper mapper,
                              IUsuario usuarioLogado) 
            : base(service, mapper)
        {
            _service = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os times do usuário logado
        /// </summary>
        /// <returns>Retorna todos os times do usuário logado</returns>
        public override ActionResult<ICollection<TimeViewModel>> ConsultarTodos()
        {
            var lista = _service.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<TimeViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Adiciona um usuário administrador ao time
        /// </summary>
        /// <param name="timeService"></param>
        /// <param name="timeId"></param>
        /// <param name="emailViewModel"></param>
        /// <returns></returns>
        [HttpPost("{timeId:guid}/administradores")]
        [ProducesResponseType(typeof(ICollection<UsuarioBasicViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarAdmin([FromServices] ITimeService timeService, 
                                                       [FromServices] IMapper autoMapper,
                                                       Guid timeId, 
                                                       EmailViewModel emailViewModel)
        {
            var email = new Email(emailViewModel.Endereco);
            var adminVO = await timeService.AdicionarAdmin(timeId, email);

            if (timeService.Invalid)
                return BadRequest(timeService.Notifications);

            var adminViewModel = autoMapper.Map<UsuarioBasicViewModel>(adminVO);

            return Ok(adminViewModel);
        }

        /// <summary>
        /// Remove um administrador do time
        /// </summary>
        /// <param name="timeService"></param>
        /// <param name="timeId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/administradores/{adminId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirAdmin([FromServices] ITimeService timeService,
                                                     Guid timeId,
                                                     Guid adminId)
        {
            await timeService.ExcluirAdmin(timeId, adminId);

            if (timeService.Invalid)
                return BadRequest(timeService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona um colaborador ao time
        /// </summary>
        /// <param name="timeService"></param>
        /// <param name="timeId"></param>
        /// <param name="emailViewModel"></param>
        /// <returns></returns>
        [HttpPost("{timeId:guid}/colaboradores")]
        [ProducesResponseType(typeof(ICollection<UsuarioBasicViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarColaborador([FromServices] ITimeService timeService,
                                                             [FromServices] IMapper autoMapper,
                                                             Guid timeId,
                                                             EmailViewModel emailViewModel)
        {
            var email = new Email(emailViewModel.Endereco);
            var colabVO = await timeService.AdicionarColaborador(timeId, email);

            if (timeService.Invalid)
                return BadRequest(timeService.Notifications);

            var colabViewModel = autoMapper.Map<UsuarioBasicViewModel>(colabVO);

            return Ok(colabViewModel);
        }

        /// <summary>
        /// Remove um colaborador do time
        /// </summary>
        /// <param name="timeService"></param>
        /// <param name="timeId"></param>
        /// <param name="colabId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/colaboradores/{colabId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirColaborador([FromServices] ITimeService timeService,
                                                           Guid timeId,
                                                           Guid colabId)
        {
            await timeService.ExcluirColaborador(timeId, colabId);

            if (timeService.Invalid)
                return BadRequest(timeService.Notifications);

            return Ok();
        }

        //public override async Task<ActionResult<Guid>> Post(TimeViewModel novaEntidadeViewModel)
        //{
        //    if (novaEntidadeViewModel.UsuarioId != _usuarioLogado.Id)
        //        return CustomBadRequest(nameof(Usuario), "Usuário inválido.");

        //    return await base.Post(novaEntidadeViewModel);
        //}       

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa")]
        [ProducesResponseType(typeof(ICollection<TimeViewModel>), StatusCodes.Status200OK)]
        public override ActionResult<ICollection<TimeViewModel>> Pesquisar([FromQuery] string filtro)
        {
            var lista = _service.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<TimeViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        protected override ICollection<TimeViewModel> Ordenar(ICollection<TimeViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();
    }
}
