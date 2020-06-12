using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.ValueObjects
{
    public abstract class BasicVO : ValueObject<BasicVO>
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        private BasicVO()
        {

        }

        public BasicVO(Guid id, string nome)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome não deve ser nulo ou vazio")
                .IsNotEmpty(id, nameof(Id), "Id não deve ser vazio")
                );

            Id = id;
            Nome = nome;
        }

        public void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(nome), "Nome não deve ser nulo ou vazio");
            else
                Nome = nome;
        }

        public override string ToString() => Nome;
    }
}
