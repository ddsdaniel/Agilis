using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Sprints;
using Agilis.Application.Services.Sprints;

namespace Agilis.WebAPI.Controllers
{
    public class SprintController : CrudController<SprintViewModel, SprintViewModel, Sprint>
    {
        private readonly SprintCrudAppService _sprintCrudAppService;

        public SprintController(SprintCrudAppService crudAppService) : base(crudAppService)
        {
            _sprintCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<SprintViewModel[]> Consultar()
        {
            var sprintsViewModel = _sprintCrudAppService.ConsultarTodos();

            if (_sprintCrudAppService.Invalido)
                return CustomBadRequest(_sprintCrudAppService);

            return Ok(sprintsViewModel);
        }
    }
}
