using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities.Seguranca;

namespace Agilis.WebAPI.Controllers
{
    [AllowAnonymous]
    public class DispositivoAnonymousController : AgilisController
    {
        private readonly IServiceProvider _serviceProvider;

        public DispositivoAnonymousController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpDelete("{usuarioId:guid}/{dispositivoId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid usuarioId, Guid dispositivoId)
        {
            using var scope = _serviceProvider.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

            var usuarioRepository = unitOfWork.ObterRepository<Usuario>();

            var usuario = await usuarioRepository.ConsultarPorIdAsync(usuarioId);

            if (usuario != null)
            {
                var dispositivoRepository = unitOfWork.ObterRepository<Dispositivo>();
                await dispositivoRepository.ExcluirAsync(dispositivoId);
            }

            return Ok();
        }
    }
}
