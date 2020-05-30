using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.WebAPI.ViewModels.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Abstractions.Services.Trabalho;
using System.Collections.Generic;
using System.Linq;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Threading.Tasks;
using System;
using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.WebAPI.Controllers.Trabalho
{
    /// <summary>
    /// Manutenção do repositório
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : CrudController<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        private readonly IProdutoService _service;
        private readonly IUsuario _usuarioLogado;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="service">Serviço para manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="usuarioLogado">Injetado a partir de IHttpContextAccessor</param>
        public ProdutosController(IProdutoService service, 
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

            var lista = _service.ConsultarTodos().OrderBy(p => p.Nome);

            var listaViewModel = _mapper.Map<List<ProdutoViewModel>>(lista);

            return Ok(listaViewModel);
        }

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<ProdutoViewModel> Ordenar(ICollection<ProdutoViewModel> lista)
                => lista.OrderBy(p => p.Nome)
                        .ToList();
    }
}
