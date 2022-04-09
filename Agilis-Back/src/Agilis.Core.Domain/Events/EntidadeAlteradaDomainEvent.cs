using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Events;
using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Extensions;

namespace Agilis.Core.Domain.Events
{
    public class EntidadeAlteradaDomainEvent<TEntity> : DomainEvent
        where TEntity : Entidade
    {
        public TEntity Antes { get; private set; }
        public TEntity Depois { get; private set; }

        public EntidadeAlteradaDomainEvent(TEntity antes, TEntity depois)
        {
            Antes = antes;
            Depois = depois;

            AddNotifications(new Contract()
                .IsValid(antes, nameof(Antes))
                .IsValid(depois, nameof(Depois))
                );
        }
    }
}
