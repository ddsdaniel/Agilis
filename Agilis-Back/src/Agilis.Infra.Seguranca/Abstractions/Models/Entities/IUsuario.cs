using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using System;

namespace Agilis.Infra.Seguranca.Abstractions.Models.Entities
{
    public interface IUsuario
    {
        Guid Id { get; }
        bool Ativo { get; }
        Email Email { get; }
        bool LicencaCompleta { get; }
        string Nome { get; }
        string NomeCompleto { get; }
        RegraUsuario Regra { get; }
        Senha Senha { get; }
        string Sobrenome { get; }

        string ToString();
    }
}