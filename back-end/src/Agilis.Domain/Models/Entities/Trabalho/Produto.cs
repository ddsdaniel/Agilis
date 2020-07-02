using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public Guid TimeId { get; set; }
        public string Nome { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome, Guid timeId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(timeId, nameof(TimeId), "O id do time não pode ser vazio")
                );

            Nome = nome;
            TimeId = timeId;
        }

        public override string ToString() => Nome;
    }
}
