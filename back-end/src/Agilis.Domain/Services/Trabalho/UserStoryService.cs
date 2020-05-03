using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class UserStoryService : CrudService<UserStory>, IUserStoryService
    {
        public UserStoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UserStoryRepository)
        {
            
        }

        public override async Task Adicionar(UserStory entity)
        {
            if (_unitOfWork.MilestoneRepository.ConsultarSeExiste(entity.Milestone.Id) == false)
                AddNotification(nameof(entity.Milestone), "MILESTONE_NAO_ENCONTRADO");
            else if (_unitOfWork.AtorRepository.ConsultarSeExiste(entity.Ator.Id) == false)
                AddNotification(nameof(entity.Ator), "ATOR_NAO_ENCONTRADO");
            else
                await base.Adicionar(entity);
        }
    }
}
