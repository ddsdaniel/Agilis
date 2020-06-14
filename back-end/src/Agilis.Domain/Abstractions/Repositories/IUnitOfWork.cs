using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Trabalho;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        IUserStoryRepository UserStoryRepository { get; }
        ITimeRepository TimeRepository { get; }
        ISprintRepository SprintRepository { get; }
        IAtorRepository AtorRepository { get; }
        IMilestoneRepository MilestoneRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IReleaseRepository ReleaseRepository { get; }
        Task Commit();
    }
}
