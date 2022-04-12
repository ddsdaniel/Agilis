using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;
using Agilis.Application.Services.Produtos;

namespace Agilis.WebAPI.Controllers
{
    public class ProdutoController : CrudController<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        private readonly ProdutoCrudAppService _produtoCrudAppService;

        public ProdutoController(ProdutoCrudAppService crudAppService) : base(crudAppService)
        {
            _produtoCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<ProdutoViewModel[]> Consultar()
        {
            var produtosViewModel = _produtoCrudAppService.ConsultarTodos();

            if (_produtoCrudAppService.Invalido)
                return CustomBadRequest(_produtoCrudAppService);

            return Ok(produtosViewModel);
        }
    }
}
