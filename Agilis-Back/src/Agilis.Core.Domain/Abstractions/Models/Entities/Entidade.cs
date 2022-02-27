using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Abstractions.Models.Entities
{
    public abstract class Entidade : EventContainer
    {
        public Guid Id { get; protected set; }

        public DateTime DataCriacao { get; protected set; }

        public DateTime DataUltimaAlteracao { get; protected set; }

        public virtual bool PodeExcluir => true;

        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            DataUltimaAlteracao = DataCriacao;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entidade;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entidade a, Entidade b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entidade a, Entidade b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }
    }
}
