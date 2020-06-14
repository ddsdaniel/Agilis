using DDS.Domain.Core.Model.ValueObjects;
using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.ValueObjects.Pessoas
{
    public class UsuarioVO : BasicVO
    {
        public Email Email { get; private set; }

        public UsuarioVO(Guid id, string nome, Email email) : base(id, nome)
        {
            AddNotifications(new Contract()
                .IsNotNull(email, nameof(Email), "E-mail não deve ser nulo")
                .IfNotNull(email, c => c.Join(email))
                );

            Email = email;
        }
    }
}
