using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class MilestoneService : CrudService<Milestone>, IMilestoneService
    {

        public MilestoneService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.MilestoneRepository)
        {
            
        }

        public override ICollection<Milestone> Pesquisar(string filtro)
            => _unitOfWork.MilestoneRepository
                    .AsQueryable()
                    .Where(m => m.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(m => m.Nome)
                    .ToList();
    }
}
