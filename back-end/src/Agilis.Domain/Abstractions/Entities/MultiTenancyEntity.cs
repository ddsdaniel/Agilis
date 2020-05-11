using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.Entities;

namespace Agilis.Domain.Abstractions.Entities
{
    public abstract class MultiTenancyEntity : Entity
    {
        public Usuario Usuario { get; private set; }
        
        protected MultiTenancyEntity(Usuario usuario)
        {
            Usuario = usuario;
        }
    }
}
