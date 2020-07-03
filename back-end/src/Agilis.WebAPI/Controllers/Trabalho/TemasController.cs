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
    public class TemasController : CrudController<TemaViewModel, TemaViewModel, Tema>
    {
        private readonly ITemaService _temaService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public TemasController(ITemaService service,
                               IMapper mapper,
                               IUsuario usuarioLogado)
            : base(service, mapper)
        {
            _temaService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os temas do usuário logado
        /// </summary>
        /// <returns>Retorna todos os temas do usuário logado</returns>
        public override ActionResult<ICollection<TemaViewModel>> ConsultarTodos()
        {
            var lista = _temaService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<TemaViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <param name="timeId">Filtra temas pelo id do time</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa-crud")]
        [ProducesResponseType(typeof(ICollection<TemaViewModel>), StatusCodes.Status200OK)]
        public ActionResult<ICollection<TemaViewModel>> Pesquisar([FromQuery] string filtro,
                                                                     [FromQuery] string timeId)
        {
            var lista = _temaService.Pesquisar(filtro, Guid.Parse(timeId), _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<TemaViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do tema
        /// </summary>
        /// <param name="lista">Lista de temas a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override ICollection<TemaViewModel> Ordenar(ICollection<TemaViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

    }
}
