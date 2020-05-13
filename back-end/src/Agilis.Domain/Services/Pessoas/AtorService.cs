using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Pessoas
{
    public class AtorService : CrudService<Ator>, IAtorService
    {

        public AtorService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.AtorRepository)
        {
        }

        public override Task Adicionar(Ator entity)
        {
            
            return base.Adicionar(entity);
        }
    }
}
