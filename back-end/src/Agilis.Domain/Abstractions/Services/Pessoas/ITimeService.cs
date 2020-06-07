using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Pessoas;
using System.Collections.Generic;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using DDS.Domain.Core.Model.ValueObjects;
using System;
using System.Threading.Tasks;
using Agilis.Domain.Models.ValueObjects.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Pessoas
{
    public interface ITimeService : ICrudService<Time>
    {
        ICollection<Time> ConsultarTodos(IUsuario usuario);
        ICollection<Time> Pesquisar(string filtro, IUsuario usuario);
        Task<UsuarioVO> AdicionarAdmin(Guid timeId, Email email);
        Task ExcluirAdmin(Guid timeId, Guid adminId);
        Task<UsuarioVO> AdicionarColaborador(Guid timeId, Email email);
        Task ExcluirColaborador(Guid timeId, Guid colabId);
    }
}
