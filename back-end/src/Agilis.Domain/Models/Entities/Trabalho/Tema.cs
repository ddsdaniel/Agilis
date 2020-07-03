using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Tema : Entity
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; private set; }

        protected Tema()
        {

        }

        public Tema(string nome, Guid produtoId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(produtoId, nameof(ProdutoId), "O id do produto não pode ser vazio")
                );

            Nome = nome;
            ProdutoId = produtoId;
        }

        public override string ToString() => Nome;
    }
}
