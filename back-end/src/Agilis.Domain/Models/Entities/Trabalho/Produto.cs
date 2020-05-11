using Agilis.Domain.Abstractions.Entities;
using Agilis.Domain.Models.Entities.Pessoas;
using Flunt.Validations;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : MultiTenancyEntity
    {
        public string Nome { get; private set; }

        public Produto(Usuario usuario, string nome) : base(usuario)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuario, nameof(Usuario), "Usuário não deve ser nulo")
                .IfNotNull(usuario, c => c.Join(usuario))
                );

            Nome = nome;
        }

    }
}
