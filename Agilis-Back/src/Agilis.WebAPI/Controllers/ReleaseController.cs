using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Releases;
using Agilis.Application.Services.Releases;

namespace Agilis.WebAPI.Controllers
{
    public class ReleaseController : CrudController<ReleaseViewModel, ReleaseViewModel, Release>
    {
        private readonly ReleaseCrudAppService _releaseCrudAppService;

        public ReleaseController(ReleaseCrudAppService crudAppService) : base(crudAppService)
        {
            _releaseCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<ReleaseViewModel[]> Consultar()
        {
            var releasesViewModel = _releaseCrudAppService.ConsultarTodos();

            if (_releaseCrudAppService.Invalido)
                return CustomBadRequest(_releaseCrudAppService);

            return Ok(releasesViewModel);
        }
    }
}
