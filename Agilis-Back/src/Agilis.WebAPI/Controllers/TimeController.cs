using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Times;
using Agilis.Application.Services.Times;

namespace Agilis.WebAPI.Controllers
{
    public class TimeController : CrudController<TimeViewModel, TimeViewModel, Time>
    {
        private readonly TimeCrudAppService _timeCrudAppService;

        public TimeController(TimeCrudAppService crudAppService) : base(crudAppService)
        {
            _timeCrudAppService = crudAppService;
        }


        [HttpGet]
        public ActionResult<TimeViewModel[]> Consultar()
        {
            var timesViewModel = _timeCrudAppService.ConsultarTodos();

            if (_timeCrudAppService.Invalido)
                return CustomBadRequest(_timeCrudAppService);

            return Ok(timesViewModel);
        }
    }
}
