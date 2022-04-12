using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Sprints;

namespace Agilis.Application.Services.Sprints
{
    public class SprintCrudAppService
        : CrudAppService<SprintViewModel, SprintViewModel, Sprint>
    {
        public SprintCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Sprint>(), unitOfWork)
        {
        }
    }
}
