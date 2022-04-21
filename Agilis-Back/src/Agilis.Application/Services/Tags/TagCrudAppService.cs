using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Application.ViewModels.Tags;
using Agilis.Core.Domain.Models.Entities.Tags;

namespace Agilis.Application.Services.Tags
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
