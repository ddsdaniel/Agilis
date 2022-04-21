using Agilis.WebAPI.Abstractions.Controllers;
using Agilis.Application.ViewModels.Tags;
using Agilis.Application.Services.Tags;
using Agilis.Core.Domain.Models.Entities.Tags;

namespace Agilis.WebAPI.Controllers
{
    public class TagController : ConsultaController<TagViewModel, Tag>
    {
        public TagController(TagCrudAppService crudAppService) : base(crudAppService)
        {
        }
    }
}
