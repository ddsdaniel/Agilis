using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Release : Entidade
    {
        public string Nome { get; private set; }
        public string Observacoes { get; private set; }
        public Produto Produto { get; private set; }

        protected Release() { }

        public Release(string nome, string observacoes, Produto produto)
        {
            Nome = nome;
            Observacoes = observacoes;
            Produto = produto;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome)) Criticar("Nome inválido");
            if (Observacoes == null) Criticar("Observações não deve ser nulo");
            if (Produto == null) Criticar("Produto não deve ser nulo");
            ImportarCriticas(Produto);
        }

        public override string ToString() => Nome;
    }
}
