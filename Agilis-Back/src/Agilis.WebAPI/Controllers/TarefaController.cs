using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Application.Services.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;

namespace Agilis.WebAPI.Controllers
{
    public class TarefaController : CrudController<TarefaViewModel, TarefaViewModel, Tarefa>
    {
        private readonly TarefaCrudAppService _tarefaCrudAppService;

        public TarefaController(TarefaCrudAppService crudAppService) : base(crudAppService)
        {
            _tarefaCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<TarefaViewModel[]> Consultar()
        {
            var tarefasViewModel = _tarefaCrudAppService.ConsultarTodos();

            if (_tarefaCrudAppService.Invalido)
                return CustomBadRequest(_tarefaCrudAppService);

            return Ok(tarefasViewModel);
        }
    }
}
