using Agilis.Domain.Abstractions.Entities;
using Agilis.Domain.Enums;
using Flunt.Validations;
using System;
using System.Threading.Tasks;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Time : MultiTenancyEntity
    {
        public string Nome { get; private set; }
        public bool Favorito { get; private set; }
        public EscopoTime Escopo { get; private set; }

        protected Time() : base(Guid.Empty)
        {

        }

        public Time(Guid usuarioId, string nome, bool favorito, EscopoTime escopo) : base(usuarioId)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuarioId, nameof(Usuario), "ID do usuário não deve ser nulo")
                .IsNotEmpty(usuarioId, nameof(Usuario), "ID do usuário não deve ser vazio")
                );

            Nome = nome;
            Favorito = favorito;
            Escopo = escopo;
        }

        internal void Desfavoritar()
        {
            if (Valid)
                Favorito = false;
        }

        public void Favoritar()
        {
            if (Valid)
                Favorito = true;
        }
    }
}
