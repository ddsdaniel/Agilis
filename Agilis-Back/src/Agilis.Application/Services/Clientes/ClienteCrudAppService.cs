using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Clientes;
using MediatR;

namespace Agilis.Application.Services.Clientes
{
    public class ClienteCrudAppService
        : CrudAppService<ClienteViewModel, ClienteViewModel, Cliente>
    {
        public ClienteCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Cliente>(), unitOfWork, mediator)
        {
        }
    }
}
