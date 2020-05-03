using Agilis.Domain.Abstractions.Repositories.Seguranca;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }
        Task Commit();
    }
}
