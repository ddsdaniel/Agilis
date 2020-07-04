using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class UserStoryService : CrudService<UserStory>, IUserStoryService
    {
        public UserStoryService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UserStoryRepository)
        {
            
        }

        public override ICollection<UserStory> Pesquisar(string filtro)
             => _unitOfWork.UserStoryRepository
                    .AsQueryable()
                    .Where(us => us.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(us => us.Nome)
                    .ToList();
    }
}
