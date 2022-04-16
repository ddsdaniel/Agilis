using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;
using Agilis.Application.Services.Produtos;

namespace Agilis.WebAPI.Controllers
{
    public class EpicoController : CrudController<EpicoViewModel, EpicoViewModel, Epico>
    {
        private readonly EpicoCrudAppService _epicoCrudAppService;

        public EpicoController(EpicoCrudAppService crudAppService) : base(crudAppService)
        {
            _epicoCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<EpicoViewModel[]> Consultar()
        {
            var epicosViewModel = _epicoCrudAppService.ConsultarTodos();

            if (_epicoCrudAppService.Invalido)
                return CustomBadRequest(_epicoCrudAppService);

            return Ok(epicosViewModel);
        }
    }
}
