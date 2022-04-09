using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Abstractions.Events
{
    public abstract class EntidadeDomainEvent<TEntity> : DomainEvent
        where TEntity : Entidade
    {
        public TEntity Entidade { get; private set; }

        public EntidadeDomainEvent(TEntity entidade)
        {
            Entidade = entidade;

            ImportarCriticas(Entidade);
        }
    }
}
