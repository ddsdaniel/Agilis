using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using MediatR;
using Agilis.Application.ViewModels;

namespace Agilis.Application.Services.Anexos
{
    public class AnexoCrudAppService
        : CrudAppService<AnexoViewModel, AnexoViewModel, Anexo>
    {
        public AnexoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Anexo>(), unitOfWork, mediator)
        {
        }
    }
}
