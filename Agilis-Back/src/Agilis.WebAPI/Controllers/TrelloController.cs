using Agilis.Infra.Importacao.Trello.Abstractions.Services;
using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            await _importacaoTrelloService.ImportarAsync();

            if (_importacaoTrelloService.Invalido)
                return CustomBadRequest(_importacaoTrelloService);

            return Ok();
        }
    }
}
