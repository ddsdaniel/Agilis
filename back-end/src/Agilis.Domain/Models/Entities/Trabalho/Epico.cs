using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Epico : Entity
    {
        public Guid TemaId { get; set; }
        public string Nome { get; private set; }

        protected Epico()
        {

        }

        public Epico(string nome, Guid temaId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotEmpty(temaId, nameof(TemaId), "O id do tema não pode ser vazio")
                );

            Nome = nome;
            TemaId = temaId;
        }

        public override string ToString() => Nome;
    }
}
