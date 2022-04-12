using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Clientes;
using Agilis.Application.Services.Clientes;

namespace Agilis.WebAPI.Controllers
{
    public class ClienteController : CrudController<ClienteViewModel, ClienteViewModel, Cliente>
    {
        private readonly ClienteCrudAppService _clienteCrudAppService;

        public ClienteController(ClienteCrudAppService crudAppService) : base(crudAppService)
        {
            _clienteCrudAppService = crudAppService;
        }

        [HttpGet]
        public ActionResult<ClienteViewModel[]> Consultar()
        {
            var clientesViewModel = _clienteCrudAppService.ConsultarTodos();

            if (_clienteCrudAppService.Invalido)
                return CustomBadRequest(_clienteCrudAppService);

            return Ok(clientesViewModel);
        }
    }
}
