using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Domain.Core.Abstractions.Services;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IReleaseService : ICrudService<Release>
    {
        ICollection<Release> ConsultarTodos(IUsuario usuario);
        ICollection<Release> Pesquisar(string filtro, IUsuario usuario);
    }
}
