using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using System;

namespace Agilis.Core.Domain.Abstractions.Models.Entities
{
    public interface IUsuario
    {
        Guid Id { get; }
        bool Ativo { get; }
        Email Email { get; }
        string Nome { get; }
        string NomeCompleto { get; }
        RegraUsuario Regra { get; }
        Senha Senha { get; }
        string Sobrenome { get; }

        string ToString();
    }
}