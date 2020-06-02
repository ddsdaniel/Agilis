using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ReleasesController : CrudController<ReleaseViewModel, ReleaseViewModel, Release>
    {
        private readonly IReleaseService _service;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        public ReleasesController(IReleaseService service, 
                                 IMapper mapper) 
            : base(service, mapper)
        {
            _service = service;
        }

        /// <summary>
        /// Consulta todos os releases do usuário logado
        /// </summary>
        /// <returns>Retorna todas as releases do usuário logado</returns>
        public override ActionResult<ICollection<ReleaseViewModel>> ConsultarTodos()
        {

            var lista = _service.ConsultarTodos().OrderBy(p => p.Nome);

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
