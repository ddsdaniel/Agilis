using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.Services.Notificacoes;
using Agilis.Application.ViewModels.Mensagens;

namespace Agilis.WebAPI.Controllers
{
    public class DispositivoController : CrudController<DispositivoViewModel, DispositivoViewModel, Dispositivo>
    {
        private readonly DispositivoCrudAppService _dispositivoCrudAppService;

        public DispositivoController(DispositivoCrudAppService crudAppService) : base(crudAppService)
        {
            _dispositivoCrudAppService = crudAppService;
        }


        [HttpGet]
        public ActionResult<DispositivoViewModel[]> Consultar()
        {
            var dispositivosViewModel = _dispositivoCrudAppService.ConsultarTodos();

            if (_dispositivoCrudAppService.Invalid)
                return CustomBadRequest(_dispositivoCrudAppService);

            return Ok(dispositivosViewModel);
        }
    }
}
