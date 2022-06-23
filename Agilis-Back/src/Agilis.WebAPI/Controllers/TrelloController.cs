using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.Infra.Importacao.Trello.ViewModels;
using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Agilis.WebAPI.Controllers
{
    public class TrelloController : AgilisController
    {
        private readonly IImportacaoTrelloService _importacaoTrelloService;

        public TrelloController(
            IImportacaoTrelloService importacaoTrelloService
            )
        {
            _importacaoTrelloService = importacaoTrelloService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ImportacaoViewModel importacaoViewModel)
        {
            await _importacaoTrelloService.ImportarAsync(importacaoViewModel);

            if (_importacaoTrelloService.Invalido)
                return CustomBadRequest(_importacaoTrelloService);

            return Ok();
        }
    }
}
