using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Arquivo : Entidade
    {
        public string Conteudo { get; private set; }
        public string Nome { get; private set; }

        protected Arquivo() { }

        public Arquivo(string conteudo, string nome)
        {
            Conteudo = conteudo;
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(Conteudo))
                Criticar("Conteúdo inválido");

            if (String.IsNullOrWhiteSpace(Nome))
                Criticar("Nome inválido");
        }

        public override string ToString() => Id.ToString();
    }
}
