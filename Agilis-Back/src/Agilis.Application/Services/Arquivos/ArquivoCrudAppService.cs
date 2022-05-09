using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using MediatR;
using Agilis.Application.ViewModels;

namespace Agilis.Application.Services.Arquivos
{
    public class ArquivoCrudAppService
        : CrudAppService<ArquivoViewModel, ArquivoViewModel, Arquivo>
    {
        public ArquivoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Arquivo>(), unitOfWork, mediator)
        {
        }
    }
}
