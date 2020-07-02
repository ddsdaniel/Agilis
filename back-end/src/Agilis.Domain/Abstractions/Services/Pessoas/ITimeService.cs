using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Model.ValueObjects;
using System;
using System.Threading.Tasks;
using Agilis.Domain.Models.ForeignKeys.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Pessoas
{
    public interface ITimeService : ICrudSeguroService<Time>
    {
        Task<UsuarioFK> AdicionarAdmin(Guid timeId, Email email);
        Task ExcluirAdmin(Guid timeId, Guid adminId);
        
        Task<UsuarioFK> AdicionarColaborador(Guid timeId, Email email);
        Task ExcluirColaborador(Guid timeId, Guid colabId);        

    }
}
