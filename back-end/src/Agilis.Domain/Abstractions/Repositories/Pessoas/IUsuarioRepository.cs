using DDS.Domain.Core.Abstractions.Repositories;
using DDS.Domain.Core.Model.ValueObjects;
using Agilis.Domain.Models.Entities.Pessoas;

namespace Agilis.Domain.Abstractions.Repositories.Pessoas
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {        
        public Usuario ConsultarPorEmail(Email email);
    }
}
