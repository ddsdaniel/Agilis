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

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class EpicosController : CrudController<EpicoViewModel, EpicoViewModel, Epico>
    {
        private readonly IEpicoService _epicoService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public EpicosController(IEpicoService service,
                               IMapper mapper,
                               IUsuario usuarioLogado)
            : base(service, mapper)
        {
            _epicoService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os epicos do usuário logado
        /// </summary>
        /// <returns>Retorna todos os epicos do usuário logado</returns>
        public override ActionResult<ICollection<EpicoViewModel>> ConsultarTodos()
        {
            var lista = _epicoService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<EpicoViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <param name="temaId">Filtra epicos pelo id do tema</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa-crud")]
        [ProducesResponseType(typeof(ICollection<EpicoViewModel>), StatusCodes.Status200OK)]
        public ActionResult<ICollection<EpicoViewModel>> Pesquisar([FromQuery] string filtro,
                                                                   [FromQuery] string temaId)
        {
            var lista = _epicoService.Pesquisar(filtro, Guid.Parse(temaId), _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<EpicoViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do epico
        /// </summary>
        /// <param name="lista">Lista de epicos a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override ICollection<EpicoViewModel> Ordenar(ICollection<EpicoViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

    }
}
