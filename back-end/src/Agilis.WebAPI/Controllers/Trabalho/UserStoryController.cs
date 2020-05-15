using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.WebAPI.ViewModels.Pessoas;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class UserStoryController : CrudController<UserStoryViewModel, UserStoryViewModel, UserStory>
    {
        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        public UserStoryController(IUserStoryService service, 
                                   IMapper mapper) 
            : base(service, mapper)
        {            
        }

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<UserStoryViewModel> Ordenar(ICollection<UserStoryViewModel> lista)
                => lista.OrderBy(us => us.Nome)
                        .ToList();

    }
}
