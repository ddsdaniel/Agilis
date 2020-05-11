using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.Domain.Services.Pessoas
{
    public class TimeService : MultiTenancyCrudService<Time>, ITimeService
    {
        
        public TimeService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.TimeRepository)
        {
            
        }

    }
}
