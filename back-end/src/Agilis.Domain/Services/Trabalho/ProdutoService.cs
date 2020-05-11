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
    public class ProdutoService : CrudService<Produto>, IProdutoService
    {
        
        public ProdutoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProdutoRepository)
        {
            
        }

        public override Task Adicionar(Produto entity)
        {
            
            return base.Adicionar(entity);
        }

        public ICollection<Produto> ConsultarTodos(IUsuario usuario)
            => _unitOfWork.ProdutoRepository
                .AsQueryable()
                .Where(p => p.Usuario.Id == usuario.Id)
                .ToList();
    }
}
