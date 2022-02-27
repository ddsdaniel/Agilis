using Agilis.Core.Domain.Abstractions.Events;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Events
{
    public class EntidadeAdicionadaDomainEvent<TEntity> : EntidadeDomainEvent<TEntity>
        where TEntity : Entidade
    {
        public EntidadeAdicionadaDomainEvent(TEntity entidade)
            : base(entidade)
        {
        }
    }
}
