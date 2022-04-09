using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Migration : Entidade
    {
        public string Nome { get; private set; }

        protected Migration() { }

        public Migration(string nome)
        {
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome da migration não deve ser vazio ou nulo");
        }
    }
}
