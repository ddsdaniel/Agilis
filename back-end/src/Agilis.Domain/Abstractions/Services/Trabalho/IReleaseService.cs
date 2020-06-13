using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IReleaseService : ICrudService<Release>
    {
        ICollection<Release> ConsultarTodos(IUsuario usuario);
        ICollection<Release> Pesquisar(string filtro, IUsuario usuario);

        Task<SprintVO> AdicionarSprint(Guid releaseId, Sprint sprint);
        Task ExcluirSprint(Guid releaseId, Guid sprintId);
    }
}
