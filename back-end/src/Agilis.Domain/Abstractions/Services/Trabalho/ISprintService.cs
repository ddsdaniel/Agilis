using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface ISprintService : ICrudService<Sprint>
    {
        //ICollection<Sprint> ConsultarTodos(IUsuario usuario);
        //ICollection<Sprint> Pesquisar(string filtro, IUsuario usuario);        
    }
}
