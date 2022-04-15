using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Tarefa : Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public TipoTarefa Tipo { get; private set; }

        protected Tarefa() { }

        public Tarefa(string titulo, string descricao, Guid produtoId, Produto produto, TipoTarefa tipo)
        {
            Titulo = titulo;
            Descricao = descricao;
            ProdutoId = produtoId;
            Produto = produto;
            Tipo = tipo;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (ProdutoId == Guid.Empty)
                Criticar("Produto id inválido");
        }

        public override string ToString() => Titulo;
    }
}
