using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Abstractions.Services.Pessoas;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Threading.Tasks;
using System;
using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : CrudController<TimeViewModel, TimeViewModel, Time>
    {
        private readonly ITimeService _service;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public TimeController(ITimeService service, 
                              IMapper mapper,
                              IUsuario usuarioLogado) 
            : base(service, mapper)
        {
            _service = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os times do usuário logado
        /// </summary>
        /// <returns>Retorna todos os times do usuário logado</returns>
        public override ActionResult<ICollection<TimeViewModel>> ConsultarTodos()
        {

            var lista = _service.ConsultarTodos(_usuarioLogado).OrderBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<TimeViewModel>>(lista);

            return Ok(listaViewModel);
        }

        public override async Task<ActionResult<Guid>> Post(TimeViewModel novaEntidadeViewModel)
        {
            if (novaEntidadeViewModel.UsuarioId != _usuarioLogado.Id)
                return CustomBadRequest(nameof(Usuario), "Usuário inválido.");

            return await base.Post(novaEntidadeViewModel);
        }
    }
}
