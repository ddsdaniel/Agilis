using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Pessoas
{
    public class AtorService : CrudService<Ator>, IAtorService
    {

        public AtorService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.AtorRepository)
        {
        }

        public override ICollection<Ator> Pesquisar(string filtro)
            => _unitOfWork.AtorRepository
                    .AsQueryable()
                    .Where(a => a.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(a => a.Nome)
                    .ToList();
    }
}
