using DDS.Domain.Core.Abstractions.Models.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Ator : Entity
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; private set; }

        protected Ator()
        {

        }

        public Ator(string nome, Guid produtoId)
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
