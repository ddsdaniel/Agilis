﻿using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Seguranca
{
    public class Login : ValueObject<Login>
    {
        public Email Email { get; private set; }
        public SenhaMedia Senha { get; private set; }

        public Login(Email email, SenhaMedia senha)
        {
            AddNotifications(new Contract()
                .IsNotNull(email, nameof(Email), "E-mail não deve ser nulo")
                .IsNotNull(senha, nameof(Senha), "Senha não deve ser nula")
                );

            AddNotifications(email, senha);

            Email = email;
            Senha = senha;
        }
    }
}
