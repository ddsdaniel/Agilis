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
using Agilis.WebAPI.ViewModels;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class TimesController : CrudController<TimeViewModel, TimeViewModel, Time>
    {
        private readonly ITimeService _service;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public TimesController(ITimeService service, 
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

            var lista = _service.ConsultarTodos(_usuarioLogado)
                .OrderByDescending(t => t.Favorito)
                .ThenBy(t => t.Nome);

            var listaViewModel = _mapper.Map<List<TimeViewModel>>(lista);

            return Ok(listaViewModel);
        }

        public override async Task<ActionResult<Guid>> Post(TimeViewModel novaEntidadeViewModel)
        {
            if (novaEntidadeViewModel.UsuarioId != _usuarioLogado.Id)
                return CustomBadRequest(nameof(Usuario), "Usuário inválido.");

            return await base.Post(novaEntidadeViewModel);
        }

        /// <summary>
        /// Marca/desmarca um time como favorito
        /// </summary>
        /// <param name="id">Id do time que será marcado/desmarcado como favorito</param>
        /// <param name="favorito">True para favoritar ou false para desfavoritar</param>
        /// <returns>Ok, sem parâmetros</returns>
        [HttpPatch("{id:guid}/favorito")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> Favoritar(Guid id, FavoritoViewModel favorito)
        {
            var time = await _service.ConsultarPorId(id);

            if (time == null)
                return CustomNotFound(nameof(Time), "Time não encontrado");

            if (favorito.Marcado)
                await _service.Favoritar(time);
            else
                await _service.Desfavoritar(time);

            if (_service.Invalid)
                return BadRequest(_service.Notifications); 
            
            await _service.Commit();

            return base.Ok();
        }

        /// <summary>
        /// Pesquisa sobre os registros do repositório
        /// </summary>
        /// <param name="filtro">Filtro inserido pelo usuário</param>
        /// <returns>Lista de registros correspondentes ao filtro</returns>
        [HttpGet("pesquisa")]
        [ProducesResponseType(typeof(ICollection<TimeViewModel>), StatusCodes.Status200OK)]
        public override ActionResult<ICollection<TimeViewModel>> Pesquisar([FromQuery] string filtro)
        {
            var lista = _service.Pesquisar(filtro, _usuarioLogado);

            var listaViewModel = _mapper.Map<ICollection<TimeViewModel>>(lista);

            listaViewModel = Ordenar(listaViewModel);

            return Ok(listaViewModel);
        }

        protected override ICollection<TimeViewModel> Ordenar(ICollection<TimeViewModel> lista)
                => lista.OrderByDescending(t => t.Favorito)
                        .ThenBy(t => t.Nome)
                        .ToList();
    }
}
