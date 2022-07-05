using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Release : Entidade
    {        
        public string Nome { get; private set; }
        public string Observacoes { get; private set; }
        public Produto Produto { get; private set; }
        public Versao Versao { get; private set; }

        protected Release() { }

        public Release(string nome, string observacoes, Produto produto, Versao versao)
        {
            Nome = nome;
            Observacoes = observacoes;
            Produto = produto;
            Versao = versao;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome)) Criticar("Nome inválido");

            if (Observacoes == null) Criticar("Observações não deve ser nulo");

            if (Produto == null) Criticar("Produto não deve ser nulo");
            ImportarCriticas(Produto);

            if (Versao == null) Criticar("Versão não deve ser nula");
            ImportarCriticas(Versao);
        }

        public override string ToString() => Nome;
    }
}
