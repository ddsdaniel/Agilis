using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Agilis.WebAPI.ViewModels;
using Agilis.Domain.Abstractions.Services;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Controller que provê os serviços de navegação entre os recursos da aplicação
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class NavigationMapController : GenericController
    {
        private readonly IMapper _mapper;
        private readonly INavigationMapService _navigationMapService;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="mapper">Automapper</param>
        /// <param name="navigationMapService">Serviço de mapa de navegação do site, com os dados do usuário</param>
        public NavigationMapController(IMapper mapper,
                                       INavigationMapService navigationMapService)
        {
            _mapper = mapper;
            _navigationMapService = navigationMapService;
        }

        /// <summary>
        /// Obtém o mapa de navegação do site
        /// </summary>
        /// <returns>Nó raíz da estrutura em forma de árvore</returns>
        [HttpGet]
        [ProducesResponseType(typeof(EntidadeNodoViewModel), StatusCodes.Status200OK)]
        public ActionResult<EntidadeNodoViewModel> Get()
        {
            var root = _navigationMapService.Obter();
            var rootViewModel = _mapper.Map<EntidadeNodoViewModel>(root);

            return Ok(rootViewModel);
        }
    }
}
