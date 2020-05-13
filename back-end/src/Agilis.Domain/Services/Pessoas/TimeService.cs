using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Pessoas
{
    public class TimeService : MultiTenancyCrudService<Time>, ITimeService
    {
        
        public TimeService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.TimeRepository)
        {
            
        }

        public async Task Favoritar(Time time)
        {
            time.Favoritar();
            
            await Atualizar(time);
        }


        public async Task Desfavoritar(Time time)
        {
            time.Desfavoritar();

            await Atualizar(time);
        }
    }
}
