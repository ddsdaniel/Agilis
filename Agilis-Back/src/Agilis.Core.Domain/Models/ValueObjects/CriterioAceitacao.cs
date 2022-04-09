using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class CriterioAceitacao : ValueObject<CriterioAceitacao>
    {
        public string Nome { get; private set; }
        public int Ordem { get; private set; }

        protected CriterioAceitacao() { }

        public CriterioAceitacao(string nome, int ordem)
        {
            Nome = nome;
            Ordem = ordem;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

