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

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ReleasesController : CrudController<ReleaseViewModel, ReleaseViewModel, Release>
    {
        private readonly IReleaseService _releaseService;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="usuarioLogado">Instância do usuário logado</param>
        /// <param name="mapper">Instância do Automapper</param>
        public ReleasesController(IReleaseService service,
                                 IUsuario usuarioLogado,
                                 IMapper mapper) 
            : base(service, mapper)
        {
            _releaseService = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa")]
        [ProducesResponseType(typeof(ICollection<ReleaseViewModel>), StatusCodes.Status200OK)]
        public override ActionResult<ICollection<ReleaseViewModel>> Pesquisar([FromQuery] string filtro)
        {
            var lista = _releaseService.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<ReleaseViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Consulta todos os releases do usuário logado
        /// </summary>
        /// <returns>Retorna todas as releases do usuário logado</returns>
        public override ActionResult<ICollection<ReleaseViewModel>> ConsultarTodos()
        {

            var lista = _releaseService.ConsultarTodos(_usuarioLogado).OrderBy(p => p.Nome);

            var listaViewModel = _mapper.Map<List<ReleaseViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<ReleaseViewModel> Ordenar(ICollection<ReleaseViewModel> lista)
                => lista.OrderBy(p => p.Nome)
                        .ToList();
    }
}
