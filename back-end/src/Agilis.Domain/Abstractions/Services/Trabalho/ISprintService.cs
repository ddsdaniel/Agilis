using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface ISprintService : ICrudService<Sprint>
    {
        //IEnumerable<Sprint> ConsultarTodos(IUsuario usuario);
        //IEnumerable<Sprint> Pesquisar(string filtro, IUsuario usuario);        
    }
}
