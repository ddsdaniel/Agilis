using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.WebAPI.ViewModels.Trabalho;
using AutoMapper;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using DDS.WebAPI.Models.ViewModels;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.WebAPI.ViewModels;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : CrudController<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        private readonly IProdutoService _produtoService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public ProdutosController(IProdutoService service,
                               IMapper mapper,
                               IUsuario usuarioLogado)
            : base(service, mapper)
        {
            _produtoService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os produtos do usuário logado
        /// </summary>
        /// <returns>Retorna todos os produtos do usuário logado</returns>
        public override ActionResult<ICollection<ProdutoViewModel>> ConsultarTodos()
        {
            var lista = _produtoService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<ProdutoViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <param name="timeId">Filtra produtos pelo id do time</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa-crud")]
        [ProducesResponseType(typeof(ICollection<ProdutoViewModel>), StatusCodes.Status200OK)]
        public ActionResult<ICollection<ProdutoViewModel>> Pesquisar([FromQuery] string filtro,
                                                                     [FromQuery] string timeId)
        {
            var lista = _produtoService.Pesquisar(filtro, Guid.Parse(timeId), _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<ProdutoViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do produto
        /// </summary>
        /// <param name="lista">Lista de produtos a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override ICollection<ProdutoViewModel> Ordenar(ICollection<ProdutoViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

        /// <summary>
        /// Adiciona um tema ao story mapping do produto
        /// </summary>
        /// <param name="produtoId">Id do produto em que o tema será adicionado</param>
        /// <param name="temaViewModel">Tema a ser adicionado</param>
        /// <returns></returns>
        [HttpPost("{produtoId:guid}/temas")]
        [ProducesResponseType(typeof(TemaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarTema(Guid produtoId,
                                                      TemaViewModel temaViewModel)
        {
            if (temaViewModel.Id == Guid.Empty)
                temaViewModel.Id = Guid.NewGuid();

            var tema = _mapper.Map<Tema>(temaViewModel);
            await _produtoService.AdicionarTema(produtoId, tema);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            temaViewModel = _mapper.Map<TemaViewModel>(tema);

            return Ok(temaViewModel);
        }

        /// <summary>
        /// Move uma user story para cima ou para baixo
        /// </summary>
        /// <param name="produtoId">Id do produto em que a user story se encontra</param>
        /// <param name="temaId">Id do tem em que a user story se encontra</param>
        /// <param name="epicoId">Id do épico em que a user story se encontra</param>
        /// <param name="userStoryId">Id da user story que será movida</param>
        /// <param name="origemDestino">Contém o índice anterior e o novo</param>
        /// <returns></returns>
        [HttpPatch("{produtoId:guid}/temas/{temaId:guid}/epicos/{epicoId:guid}/user-stories/{userStoryId:guid}/mover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> MoverUserStory(Guid produtoId,
                                                       Guid temaId,
                                                       Guid epicoId,
                                                       Guid userStoryId,
                                                       OrigemDestinoViewModel origemDestino)
        {
            await _produtoService.MoverUserStory(produtoId, temaId, epicoId, userStoryId, origemDestino.Destino);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Remove um tema do story mapping
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="temaId"></param>
        /// <returns></returns>
        [HttpDelete("{produtoId:guid}/temas/{temaId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirTema(Guid produtoId,
                                                     Guid temaId)
        {
            await _produtoService.ExcluirTema(produtoId, temaId);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            return Ok();
        }

        /// <summary>
        /// Adiciona um épico ao story mapping do produto
        /// </summary>
        /// <param name="produtoId">Id do produto em que o tema será adicionado</param>
        /// <param name="temaId">Id do tema em que o épico será adicionado</param>
        /// <param name="nomeContainer">Nome do épico a ser adicionado</param>
        /// <returns></returns>
        [HttpPost("{produtoId:guid}/temas/{temaId:guid}/epicos")]
        [ProducesResponseType(typeof(EpicoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarEpico(Guid produtoId,
                                                       Guid temaId,
                                                       StringContainerViewModel nomeContainer)
        {
            var epico = new Epico(Guid.NewGuid(), nomeContainer.Texto, new List<UserStoryFK>());
            await _produtoService.AdicionarEpico(produtoId, temaId, epico);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            var epicoViewModel = _mapper.Map<EpicoViewModel>(epico);

            return Ok(epicoViewModel);
        }

        /// <summary>
        /// Adiciona uma user story ao épico
        /// </summary>
        /// <param name="produtoId">Id do produto em que o tema será adicionado</param>
        /// <param name="temaId">Id do tema em que o épico será adicionado</param>
        /// <param name="epicoId">Id do tema em que o épico será adicionado</param>
        /// <param name="nomeContainer">Nome do épico a ser adicionado</param>
        /// <returns></returns>
        [HttpPost("{produtoId:guid}/temas/{temaId:guid}/epicos/{epicoId:guid}/user-stories")]
        [ProducesResponseType(typeof(UserStoryFK), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarUserStory(Guid produtoId,
                                                       Guid temaId,
                                                       Guid epicoId,
                                                       StringContainerViewModel nomeContainer)
        {
            var userStory = new UserStory(
                nome: nomeContainer.Texto, 
                ator: new AtorFK(Guid.NewGuid(), ""),
                narrativa: "-",
                objetivo: "-",
                new List<CriterioAceitacao>()
                );

            await _produtoService.AdicionarUserStory(produtoId, temaId, epicoId, userStory);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            var userStoryFK = new UserStoryFK(userStory.Id, userStory.Nome);

            return Ok(userStoryFK);
        }
    }
}
