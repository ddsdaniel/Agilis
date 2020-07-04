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

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class UserStoriesController : CrudController<UserStoryViewModel, UserStoryViewModel, UserStory>
    {
        private readonly IUserStoryService _userStoryService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public UserStoriesController(IUserStoryService service,
                               IMapper mapper,
                               IUsuario usuarioLogado)
            : base(service, mapper)
        {
            _userStoryService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os userStories do usuário logado
        /// </summary>
        /// <returns>Retorna todos os userStories do usuário logado</returns>
        public override ActionResult<ICollection<UserStoryViewModel>> ConsultarTodos()
        {
            var lista = _userStoryService.ConsultarTodos(_usuarioLogado)
                .OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<UserStoryViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <param name="epicoId">Filtra userStories pelo id do [epico</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa-crud")]
        [ProducesResponseType(typeof(ICollection<UserStoryViewModel>), StatusCodes.Status200OK)]
        public ActionResult<ICollection<UserStoryViewModel>> Pesquisar([FromQuery] string filtro,
                                                                       [FromQuery] string epicoId)
        {
            var lista = _userStoryService.Pesquisar(filtro, Guid.Parse(epicoId), _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<UserStoryViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Ordena pelo nome do userStory
        /// </summary>
        /// <param name="lista">Lista de userStories a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override ICollection<UserStoryViewModel> Ordenar(ICollection<UserStoryViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

    }
}
