using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using Agilis.Domain.Abstractions.Entities.Pessoas;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudService<Produto>
    {
        ICollection<Produto> ConsultarTodos(IUsuario usuario);
    }
}
