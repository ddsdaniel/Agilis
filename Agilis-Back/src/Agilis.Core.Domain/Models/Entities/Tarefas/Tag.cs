using Agilis.Core.Domain.Abstractions.Models.Entities;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class Tag : Entidade
    {
        public string Nome { get; private set; }

        protected Tag() { }

        public Tag(string nome)
        {
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

