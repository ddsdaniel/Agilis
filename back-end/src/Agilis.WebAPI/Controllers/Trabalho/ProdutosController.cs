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
        /// <param name="nomeContainer">Nome do tema a ser adicionado</param>
        /// <returns></returns>
        [HttpPost("{produtoId:guid}/temas")]
        [ProducesResponseType(typeof(TemaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AdicionarTema(Guid produtoId,
                                                      StringContainerViewModel nomeContainer)
        {
            var tema = new Tema(Guid.NewGuid(), nomeContainer.Texto, new List<Epico>());
            await _produtoService.AdicionarTema(produtoId, tema);

            if (_produtoService.Invalid)
                return BadRequest(_produtoService.Notifications);

            var temaViewModel = _mapper.Map<TemaViewModel>(tema);

            return Ok(temaViewModel);
        }

        /// <summary>
        /// Adiciona um épico ao story mapping do produto
        /// </summary>
        /// <param name="produtoId">Id do produto em que o tema será adicionado</param>
        /// <param name="temaId">Id do tema em que o épico será adicionado</param>
        /// <param name="nomeContainer">Nome do épico a ser adicionado</param>
        /// <returns></returns>
        [HttpPost("{produtoId:guid}/temas/{temaId:guid}")]
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
    }
}
