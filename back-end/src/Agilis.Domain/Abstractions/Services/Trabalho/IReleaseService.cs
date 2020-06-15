using DDS.Domain.Core.Abstractions.Services;
using System.Threading.Tasks;
using Agilis.Domain.Models.ForeignKeys;
using System;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IReleaseService : IService
    {
        Task<SprintFK> AdicionarSprint(Guid releaseId, string nome);
        Task ExcluirSprint(Guid releaseId, Guid sprintId);
    }
}
