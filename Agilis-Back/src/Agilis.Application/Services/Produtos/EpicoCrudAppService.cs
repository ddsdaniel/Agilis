using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;

namespace Agilis.Application.Services.Produtos
{
    public class EpicoCrudAppService
        : CrudAppService<EpicoViewModel, EpicoViewModel, Epico>
    {
        public EpicoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Epico>(), unitOfWork)
        {
        }
    }
}
