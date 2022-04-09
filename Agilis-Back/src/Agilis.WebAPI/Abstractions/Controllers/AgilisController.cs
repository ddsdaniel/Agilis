using DDS.Validacoes.Abstractions.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Agilis.WebAPI.Abstractions.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AgilisController : ControllerBase
    {
        /// <summary>
        /// Monta um retorno de not found com um conteúdo formatado como uma lista de notificações, para ficar compatível com o padrão de retorno de erros do sistema
        /// </summary>
        /// <param name="property">Nome da propriedade do erro</param>
        /// <param name="message">Mensagem de erro</param>
        /// <returns>NotFoundObjectResult com uma lista de notificações de apenas um registro, o qual foi passado por parâmetro</returns>
        protected virtual NotFoundObjectResult CustomNotFound(string message)
            => NotFound(new List<string> { message });

        /// <summary>
        /// Monta um retorno de bad request com um conteúdo formatado como uma lista de notificações, para ficar compatível com o padrão de retorno de erros do sistema
        /// </summary>
        /// <param name="property">Nome da propriedade do erro</param>
        /// <param name="message">Mensagem de erro</param>
        /// <returns>BadRequestObjectResult com uma lista de notificações de apenas um registro, o qual foi passado por parâmetro</returns>
        protected virtual BadRequestObjectResult CustomBadRequest(string message)
            => BadRequest(new List<string> { message });

        protected virtual BadRequestObjectResult CustomBadRequest(IValidavel validavel)
            => CustomBadRequest(string.Join(',', validavel.Criticas));
    }
}
