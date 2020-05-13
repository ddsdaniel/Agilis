using Agilis.Domain.Abstractions.Entities;
using Agilis.Domain.Models.Entities.Pessoas;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : MultiTenancyEntity
    {
        public string Nome { get; private set; }

        protected Produto() : base(Guid.Empty)
        {

        }

        public Produto(Guid usuarioId, string nome) : base(usuarioId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuarioId, nameof(Usuario), "ID do Usuário não deve ser nulo")                
                );

            Nome = nome;
        }

    }
}
