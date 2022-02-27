using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Extensions;

namespace Agilis.Core.Domain.Abstractions.Events
{
    public abstract class EntidadeDomainEvent<TEntity> : DomainEvent
        where TEntity : Entidade
    {
        public TEntity Entidade { get; private set; }

        public EntidadeDomainEvent(TEntity entidade)
        {
            Entidade = entidade;

            AddNotifications(new Contract()
                .IsValid(entidade, nameof(Entidade))
                );
        }
    }
}
