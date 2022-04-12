using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Cliente : Entidade
    {
        public string Nome { get; private set; }
        public string IdIntegracaoSac { get; private set; }
        protected Cliente() { }

        public Cliente(string nome, string idIntegracaoSac)
        {
            Nome = nome;
            IdIntegracaoSac = idIntegracaoSac;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (IdIntegracaoSac == null)
                Criticar("Id de integração SAC inválido.");
        }

        public override string ToString() => Nome;
    }
}
