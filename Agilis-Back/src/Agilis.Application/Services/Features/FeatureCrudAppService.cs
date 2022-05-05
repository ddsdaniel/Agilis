using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using System.Linq;
using MediatR;
using Agilis.Application.ViewModels.Produtos;
using Agilis.Core.Domain.Models.ValueObjects.Produtos;

namespace Agilis.Application.Services.Features
{
    public class FeatureCrudAppService
        : CrudAppService<FeatureViewModel, FeatureViewModel, Feature>
    {
        public FeatureCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Feature>(), unitOfWork, mediator)
        {
        }

        public override FeatureViewModel[] ConsultarTodos()
        {
            return base.ConsultarTodos()
                .OrderBy(p => p.Nome)
                .ToArray();
        }
    }
}
