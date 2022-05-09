using Agilis.Application.Services.Arquivos;
using Agilis.Application.ViewModels;
using Agilis.Core.Domain.Models.Entities;
using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Agilis.WebAPI.Controllers
{
    public class ArquivoController : CrudController<ArquivoViewModel, ArquivoViewModel, Arquivo>
    {
        private readonly ArquivoCrudAppService _arquivoCrudAppService;

        public ArquivoController(ArquivoCrudAppService crudAppService) : base(crudAppService)
        {
            _arquivoCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<ArquivoViewModel[]> Consultar()
        {
            var arquivosViewModel = _arquivoCrudAppService.ConsultarTodos();

            if (_arquivoCrudAppService.Invalido)
                return CustomBadRequest(_arquivoCrudAppService);

            return Ok(arquivosViewModel);
        }
    }
}
