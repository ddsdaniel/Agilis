using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Time : Entidade
    {
        public string Nome { get; private set; }

        protected Time()  { }

        public Time(string nome)

        {
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome do time não deve ser vazio ou nulo");
        }

        public override string ToString() => Nome;
    }
}
