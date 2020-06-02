using Agilis.Domain.Enums;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Time : Entity
    {
        public string Nome { get; private set; }
        public EscopoTime Escopo { get; private set; }
        public Guid UsuarioId { get; private set; }

        protected Time()
        {

        }

        public Time(Guid usuarioId, string nome, EscopoTime escopo)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(usuarioId, nameof(Usuario), "ID do usuário não deve ser nulo")
                .IsNotEmpty(usuarioId, nameof(Usuario), "ID do usuário não deve ser vazio")
                );

            UsuarioId = usuarioId;
            Nome = nome;
            Escopo = escopo;
        }        
    }
}
