using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using Agilis.Application.Services.Tarefas;

namespace Agilis.WebAPI.Controllers
{
    public class TagController : CrudController<TagViewModel, TagViewModel, Tag>
    {
        private readonly TagCrudAppService _tagCrudAppService;

        public TagController(TagCrudAppService crudAppService) : base(crudAppService)
        {
            _tagCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<TagViewModel[]> Consultar()
        {
            var tagsViewModel = _tagCrudAppService.ConsultarTodos();

            if (_tagCrudAppService.Invalido)
                return CustomBadRequest(_tagCrudAppService);

            return Ok(tagsViewModel);
        }
    }
}
