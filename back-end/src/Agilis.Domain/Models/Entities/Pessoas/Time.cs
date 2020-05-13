using Agilis.Domain.Abstractions.Entities;
using Flunt.Validations;
using System;
using System.Threading.Tasks;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Time : MultiTenancyEntity
    {
        public string Nome { get; private set; }
        public bool Favorito { get; private set; }

        protected Time() : base(Guid.Empty)
        {

        }

        public Time(Guid usuarioId, string nome, bool favorito) : base(usuarioId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuarioId, nameof(Usuario), "ID do Usuário não deve ser nulo")                
                );

            Nome = nome;
            Favorito = favorito;
        }

        internal void Desfavoritar()
        {
            Favorito = false;
        }

        public void Favoritar()
        {
            Favorito = true;
        }
    }
}
