using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.Entities;
using System;

namespace Agilis.Domain.Abstractions.Entities
{
    public abstract class MultiTenancyEntity : Entity
    {
        public Guid UsuarioId { get; private set; }
        
        protected MultiTenancyEntity(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
