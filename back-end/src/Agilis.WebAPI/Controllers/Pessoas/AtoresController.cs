using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.WebAPI.ViewModels.Pessoas;
using AutoMapper;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class AtoresController : CrudController<AtorViewModel, AtorViewModel, Ator>
    {
        private readonly IAtorService _atorService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public AtoresController(IAtorService service,
                               IMapper mapper,
                               IUsuario usuarioLogado)
            : base(service, mapper)
        {
            _atorService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os atores do usuário logado
        /// </summary>
        /// <returns>Retorna todos os atores do usuário logado</returns>
        public override ActionResult<IEnumerable<AtorViewModel>> ConsultarTodos()
        {
            var lista = _atorService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<AtorViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <param name="produtoId">Filtra atores pelo id do produto</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa-crud")]
        [ProducesResponseType(typeof(IEnumerable<AtorViewModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AtorViewModel>> Pesquisar([FromQuery] string filtro,
                                                                  [FromQuery] string produtoId)
        {
            var lista = _atorService.Pesquisar(filtro, Guid.Parse(produtoId), _usuarioLogado);

            var listaViewModel = _mapper.Map<IEnumerable<AtorViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do ator
        /// </summary>
        /// <param name="lista">Lista de atores a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override IEnumerable<AtorViewModel> Ordenar(IEnumerable<AtorViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

    }
}
