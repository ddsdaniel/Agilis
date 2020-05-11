using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : CrudController<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        private readonly IProdutoService _service;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public ProdutoController(IProdutoService service, 
                                 IMapper mapper,
                                 IUsuario usuarioLogado) 
            : base(service, mapper)
        {
            _service = service;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Consulta todos os produtos do usuário logado
        /// </summary>
        /// <returns>Retorna todos os produtos do usuário logado</returns>
        public override ActionResult<ICollection<ProdutoViewModel>> ConsultarTodos()
        {

            var lista = _service.ConsultarTodos(_usuarioLogado);

            var listaViewModel = _mapper.Map<List<ProdutoViewModel>>(lista);

            return Ok(listaViewModel);
        }
    }
}
