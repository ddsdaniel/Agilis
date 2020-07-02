using Agilis.Domain.Abstractions.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.Entities;
using DDS.Domain.Core.Abstractions.Services;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services
{
    public interface ICrudSeguroService<TEntity> : ICrudService<TEntity> where TEntity : Entity 
    {
        ICollection<TEntity> ConsultarTodos(IUsuario usuario);
        ICollection<TEntity> Pesquisar(string filtro, IUsuario usuario);
    }
}
