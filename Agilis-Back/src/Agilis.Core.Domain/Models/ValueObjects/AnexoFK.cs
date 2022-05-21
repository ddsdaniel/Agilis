using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class AnexoFK : ValueObject<AnexoFK>
    {
        public Guid AnexoId { get; private set; }
        public string Nome { get; private set; }        

        protected AnexoFK() { }

        public AnexoFK(string nome, Guid anexoId)
        {
            AnexoId = anexoId;
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (AnexoId == Guid.Empty)
                Criticar("AnexoId inválido");

            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");
        }

        public override string ToString() => Nome;
    }
}
