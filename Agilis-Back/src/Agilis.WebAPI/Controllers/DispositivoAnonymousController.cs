using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.DependencyInjection;
using Agilis.Core.Domain.Abstractions.Factories;
using Agilis.Core.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Agilis.Infra.Seguranca.Models.Entities;

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

            var unitOfWorkFactory = scope.ServiceProvider.GetService<IUnitOfWorkFactory>();

            var unitOfWorkCatalogo = unitOfWorkFactory.ObterUnitOfWorkCatalogo();

            var usuarioRepository = unitOfWorkCatalogo.ObterRepository<Usuario>();

            var usuario = await usuarioRepository.ConsultarPorIdAsync(usuarioId);

            if (usuario != null)
            {
                var unitOfWorkInquilino = unitOfWorkFactory.ObterUnitOfWorkInquilino(usuario.Email);
                var dispositivoRepository = unitOfWorkInquilino.ObterRepository<Dispositivo>();
                await dispositivoRepository.ExcluirAsync(dispositivoId);
            }

            return Ok();
        }
    }
}
