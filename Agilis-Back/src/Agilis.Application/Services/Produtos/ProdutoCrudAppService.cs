using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;

namespace Agilis.Application.Services.Produtos
{
    public class ProdutoCrudAppService
        : CrudAppService<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        public ProdutoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Produto>(), unitOfWork)
        {
        }
    }
}
