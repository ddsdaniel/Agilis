using Agilis.Domain.Abstractions.Repositories.Pessoas;
using Agilis.Domain.Abstractions.Repositories.Trabalho;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        IUserStoryRepository UserStoryRepository { get; }
        IAtorRepository AtorRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        Task Commit();
    }
}
