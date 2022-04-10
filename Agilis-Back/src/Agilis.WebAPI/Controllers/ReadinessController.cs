using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.WebAPI.Abstractions.Controllers;
using System;
using System.Linq;
using Agilis.Core.Domain.Models.Entities.Seguranca;

namespace Agilis.WebAPI.Controllers
{
    [AllowAnonymous]
    public class ReadinessController : AgilisController
    {
        [HttpGet]
        public IActionResult Consultar([FromServices] IUnitOfWork unitOfWork)
        {
            try
            {
                var usuarioRepository = unitOfWork.ObterRepository<Usuario>();

                var achou = usuarioRepository.Consultar().Any();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(503);//serviço indisponível
            }
        }
    }
}
