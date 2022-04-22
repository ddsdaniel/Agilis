using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Application.ViewModels.Tarefas;
using Agilis.Core.Domain.Models.Entities.Tarefas;

namespace Agilis.Application.Services.Tarefas
{
    public class TagCrudAppService
        : CrudAppService<TagViewModel, TagViewModel, Tag>
    {
        public TagCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Tag>(), unitOfWork)
        {
        }
    }
}
