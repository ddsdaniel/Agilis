using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;

namespace Agilis.Domain.Services.Trabalho
{
    public class ProdutoService : MultiTenancyCrudService<Produto>, IProdutoService
    {
        
        public ProdutoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProdutoRepository)
        {
            
        }

    }
}
