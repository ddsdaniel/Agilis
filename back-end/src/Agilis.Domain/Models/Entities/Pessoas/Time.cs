using Agilis.Domain.Abstractions.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Time : MultiTenancyEntity
    {
        public string Nome { get; private set; }

        protected Time() : base(Guid.Empty)
        {

        }

        public Time(Guid usuarioId, string nome) : base(usuarioId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuarioId, nameof(Usuario), "ID do Usuário não deve ser nulo")                
                );

            Nome = nome;
        }

    }
}
