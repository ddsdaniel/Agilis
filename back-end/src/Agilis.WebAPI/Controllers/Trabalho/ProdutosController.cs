using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Agilis.Domain.Abstractions.Services.Pessoas;

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
        private readonly ITimeService _timeService;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public ProdutosController(IProdutoService service, 
                                  IMapper mapper,
                                  IUsuario usuarioLogado,
                                  ITimeService timeService) 
            : base(service, mapper)
        {
            _produtoService = service;
            _usuarioLogado = usuarioLogado;
            _timeService = timeService;
        }

        /// <summary>
        /// Consulta todos os produtos do usuário logado
        /// </summary>
        /// <returns>Retorna todos os produtos do usuário logado</returns>
        public override ActionResult<ICollection<ProdutoViewModel>> ConsultarTodos()
        {

            var lista = _produtoService.ConsultarTodos(_usuarioLogado).OrderBy(p => p.Nome);

            var listaViewModel = _mapper.Map<List<ProdutoViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Altera o produto em seu repositório em nas demais entidades com a qual se relaciona
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewModelCadastro"></param>
        /// <returns></returns>
        public override async Task<ActionResult> Put(Guid id, ProdutoViewModel viewModelCadastro)
        {
            var time = await _timeService.ConsultarPorId(viewModelCadastro.Time.Id);
            time.RenomearProduto(viewModelCadastro.Id, viewModelCadastro.Nome);
            if (time.Invalid)
                return CustomBadRequest(nameof(time), "Falha ao renomear o produto no time");
            await _timeService.Atualizar(time);
            if (_timeService.Invalid)
                return CustomBadRequest(nameof(time), "Falha ao renomear o produto no time");

            return await base.Put(id, viewModelCadastro);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa")]
        [ProducesResponseType(typeof(ICollection<ProdutoViewModel>), StatusCodes.Status200OK)]
        public override ActionResult<ICollection<ProdutoViewModel>> Pesquisar([FromQuery] string filtro)
        {
            var lista = _produtoService.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<ProdutoViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<ProdutoViewModel> Ordenar(ICollection<ProdutoViewModel> lista)
                => lista.OrderBy(p => p.Nome)
                        .ToList();
      
    }
}
