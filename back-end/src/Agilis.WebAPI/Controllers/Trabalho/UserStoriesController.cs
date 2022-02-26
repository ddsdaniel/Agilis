using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;

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
        /// Ordena pelo nome do userStory
        /// </summary>
        /// <param name="lista">Lista de userStories a ser ordenada</param>
        /// <returns>Lista já ordenada pelo nome</returns>
        protected override IEnumerable<UserStoryViewModel> Ordenar(IEnumerable<UserStoryViewModel> lista)
                => lista.OrderBy(t => t.Nome)
                        .ToList();

    }
}
