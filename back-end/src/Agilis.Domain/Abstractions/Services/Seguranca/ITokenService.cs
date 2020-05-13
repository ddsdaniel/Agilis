using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Seguranca
{
    public interface ITokenService
    {
        string Gerar(Usuario usuario);
    }
}
