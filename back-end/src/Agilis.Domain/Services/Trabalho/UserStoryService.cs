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

        public override async Task Adicionar(UserStory userStory)
        {
            if (TestarEntidadesExistes(userStory))            
                await base.Adicionar(userStory);
        }

        public override async Task Atualizar(UserStory userStory)
        {
            if (TestarEntidadesExistes(userStory))
                await base.Atualizar(userStory);
        }

        private bool TestarEntidadesExistes(UserStory userStory)
        {
            if (userStory.Milestone != null && _unitOfWork.MilestoneRepository.ConsultarSeExiste(userStory.Milestone.Id) == false)
                AddNotification(nameof(userStory.Milestone), "MILESTONE_NAO_ENCONTRADO");

            else if (_unitOfWork.AtorRepository.ConsultarSeExiste(userStory.Ator.Id) == false)
                AddNotification(nameof(userStory.Ator), "ATOR_NAO_ENCONTRADO");

            else
                return true;

            return false;
        }
    }
}
