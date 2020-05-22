using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Mocks.Entities.Pessoas;

namespace Agilis.WebAPI.Tests.Integracao.Helpers
{
    /// <summary>
    /// Seed the database with test data.
    /// </summary>
    public static class InitializeDbForTests
    {
        public static void Inicializar(IUnitOfWork unitOfWork)
        {
            var admin = UsuarioMock.ObterAdminValido();
            unitOfWork.UsuarioRepository.Adicionar(admin);
            unitOfWork.Commit();
        }
    }
}
