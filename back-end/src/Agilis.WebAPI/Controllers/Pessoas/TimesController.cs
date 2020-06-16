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
using DDS.Domain.Core.Model.ValueObjects;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.WebAPI.Models.ViewModels;
using Agilis.Domain.Models.ForeignKeys.Trabalho;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class TimesController : CrudController<TimeViewModel, TimeViewModel, Time>
    {
        private readonly ITimeService _timeService;
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
            _timeService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os times do usuário logado
        /// </summary>
        /// <returns>Retorna todos os times do usuário logado</returns>
        public override ActionResult<ICollection<TimeViewModel>> ConsultarTodos()
        {
            var lista = _timeService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<TimeViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Adiciona um usuário administrador ao time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="emailViewModel"></param>
        /// <returns></returns>
        [HttpPost("{timeId:guid}/administradores")]
        [ProducesResponseType(typeof(ICollection<UsuarioBasicViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarAdmin(Guid timeId,
                                                       EmailViewModel emailViewModel)
        {
            var email = new Email(emailViewModel.Endereco);
            var adminVO = await _timeService.AdicionarAdmin(timeId, email);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            var adminViewModel = _mapper.Map<UsuarioBasicViewModel>(adminVO);

            return Ok(adminViewModel);
        }

        /// <summary>
        /// Remove um administrador do time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="adminId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/administradores/{adminId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirAdmin(Guid timeId,
                                                     Guid adminId)
        {
            await _timeService.ExcluirAdmin(timeId, adminId);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona um colaborador ao time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="emailViewModel"></param>
        /// <returns></returns>
        [HttpPost("{timeId:guid}/colaboradores")]
        [ProducesResponseType(typeof(ICollection<UsuarioBasicViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarColaborador(Guid timeId,
                                                             EmailViewModel emailViewModel)
        {
            var email = new Email(emailViewModel.Endereco);
            var colabVO = await _timeService.AdicionarColaborador(timeId, email);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            var colabViewModel = _mapper.Map<UsuarioBasicViewModel>(colabVO);

            return Ok(colabViewModel);
        }

        /// <summary>
        /// Remove um colaborador do time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="colabId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/colaboradores/{colabId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirColaborador(Guid timeId,
                                                           Guid colabId)
        {
            await _timeService.ExcluirColaborador(timeId, colabId);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona uma release ao time
        /// </summary>
        /// <param name="timeId">Id do time em que a release será adicionada</param>
        /// <param name="nomeViewModel">Container do nome da release</param>
        /// <returns>ReleaseFK com o id e o nome da release adicionada</returns>
        [HttpPost("{timeId:guid}/releases")]
        [ProducesResponseType(typeof(ReleaseFK), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarRelease(Guid timeId,
                                                         StringContainerViewModel nomeViewModel)
        {
            var releaseFK = await _timeService.AdicionarRelease(timeId, nomeViewModel.Texto);
            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            return Ok(releaseFK);
        }        

        /// <summary>
        /// Remove um release do time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="releaseId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/releases/{prodId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirRelease(Guid timeId,
                                                       Guid releaseId)
        {
            await _timeService.ExcluirRelease(timeId, releaseId);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona um produto ao time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="produtoViewModel"></param>
        /// <returns></returns>
        [HttpPost("{timeId:guid}/produtos")]
        [ProducesResponseType(typeof(ICollection<ProdutoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarProduto(Guid timeId,
                                                         ProdutoViewModel produtoViewModel)
        {
            var produto = new Produto(produtoViewModel.Nome);
            await _timeService.AdicionarProduto(timeId, produto);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

            var produtoBasicViewModel = _mapper.Map<ProdutoViewModel>(produto);

            return Ok(produtoBasicViewModel);
        }

        /// <summary>
        /// Remove um produto do time
        /// </summary>
        /// <param name="timeId"></param>
        /// <param name="prodId"></param>
        /// <returns></returns>
        [HttpDelete("{timeId:guid}/produtos/{prodId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirProduto(Guid timeId,
                                                       Guid prodId)
        {
            await _timeService.ExcluirProduto(timeId, prodId);

            if (_timeService.Invalid)
                return BadRequest(_timeService.Notifications);

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
            var lista = _timeService.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<TimeViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do time
        /// </summary>
        /// <param name="lista">Lista de times a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override ICollection<TimeViewModel> Ordenar(ICollection<TimeViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();
    }
}
