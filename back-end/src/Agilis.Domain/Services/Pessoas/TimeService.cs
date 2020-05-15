using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Collections.Generic;
using System.Linq;
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

        public override ICollection<Time> Pesquisar(string filtro)
             => _unitOfWork.TimeRepository
                    .AsQueryable()
                    .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();

        public ICollection<Time> Pesquisar(string filtro, IUsuario usuario)
            => _unitOfWork.TimeRepository
                    .AsQueryable()
                    .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()) &&
                        t.UsuarioId == usuario.Id)
                    .OrderBy(t => t.Nome)
                    .ToList();
    }
}
