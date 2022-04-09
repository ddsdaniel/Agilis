using Agilis.Core.Domain.Abstractions.Events;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Events
{
    public class EntidadeExcluidaDomainEvent<TEntity> : EntidadeDomainEvent<TEntity>
        where TEntity : Entidade
    {
        public EntidadeExcluidaDomainEvent(TEntity entidade)
            : base(entidade)
        {
        }
    }
}
