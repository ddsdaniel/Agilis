using DDS.Validacoes.Abstractions.Models;
using System;

namespace Agilis.Core.Domain.Abstractions.Models.Entities
{
    public abstract class Entidade : Validavel
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

        public void AtualizarId(Guid id)
        {
            Id = id;
        }
    }
}
