using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Repositories;
using System.Linq;

namespace Agilis.Domain.Abstractions.Repositories.Pessoas
{
    public interface ITimeRepository : IRepository<Time>
    {
        IQueryable<Time> ObterTimes(IUsuario usuario);
    }
}
