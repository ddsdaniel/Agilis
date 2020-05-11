using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
