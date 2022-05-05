using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Produto : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string UrlRepositorio { get; private set; }

        protected Produto() { }

        public Produto(string nome, string descricao, string urlRepositorio)
        {
            Nome = nome;
            Descricao = descricao;
            UrlRepositorio = urlRepositorio;
            Validar();
        }
        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (UrlRepositorio == null)
                Criticar("URL do repositório inválida.");
        }

        public override string ToString() => Nome;
    }
}
