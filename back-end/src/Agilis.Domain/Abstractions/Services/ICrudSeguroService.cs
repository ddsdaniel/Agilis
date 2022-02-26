using Agilis.Domain.Abstractions.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Models.Entities;
using DDS.Domain.Core.Abstractions.Services;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services
{
    public interface ICrudSeguroService<TEntity> : ICrudService<TEntity> where TEntity : Entity 
    {
        IEnumerable<TEntity> ConsultarTodos(IUsuario usuario);
        IEnumerable<TEntity> Pesquisar(string filtro, IUsuario usuario);
    }
}
