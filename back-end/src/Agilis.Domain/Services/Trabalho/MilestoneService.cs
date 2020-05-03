using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class MilestoneService : CrudService<Milestone>, IMilestoneService
    {

        public MilestoneService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.MilestoneRepository)
        {
        }

        public override Task Adicionar(Milestone entity)
        {
            
            return base.Adicionar(entity);
        }
    }
}
