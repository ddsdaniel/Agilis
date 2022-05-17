using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Application.Services.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;
using System.Collections.Generic;

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

        [HttpGet("tags")]
        public ActionResult<string[]> ConsultarTags()
        {
            var tags = _tarefaCrudAppService.ConsultarTags();

            if (_tarefaCrudAppService.Invalido)
                return CustomBadRequest(_tarefaCrudAppService);

            return Ok(tags);
        }

        [HttpGet("pesquisa")]
        public ActionResult<IEnumerable<TarefaViewModel>> Consultar(
            [FromQuery] string sprintId,
            [FromQuery] string relatorId,
            [FromQuery] string solucionadorId
            )
        {
            //SituacaoTransacao? situacao = null;
            //if (filtroSituacao != "Todas")
            //{
            //    Enum.TryParse(filtroSituacao, out SituacaoTransacao sit);
            //    situacao = sit;
            //}

            var tarefasViewModel = _tarefaCrudAppService
                .Pesquisar(sprintId, relatorId, solucionadorId);

            if (_tarefaCrudAppService.Invalido)
                return CustomBadRequest(_tarefaCrudAppService);

            return Ok(tarefasViewModel);
        }
    }
}
