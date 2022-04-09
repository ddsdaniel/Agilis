using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Agilis.Application.Services.Seguranca;
using Agilis.Application.ViewModels.Seguranca;

namespace Agilis.WebAPI.Controllers
{
    public class RefreshTokenController : AgilisController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioLogadoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RefreshTokenViewModel>> Renovar(
            [FromServices] RefreshTokenAppService refreshTokenAppService,
            RefreshTokenViewModel refreshTokenViewModel
            )
        {
            var usuarioLogadoViewModel = await refreshTokenAppService.RenovarAsync(refreshTokenViewModel);

            if (refreshTokenAppService.Invalido)
                return Forbid();

            return Ok(usuarioLogadoViewModel);
        }

        [HttpGet("testar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Testar()
        {
            return Ok();
        }

    }
}
