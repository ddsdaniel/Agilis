using Agilis.Domain.Enums;
using DDS.Domain.Core.Models.ValueObjects;
using DDS.Domain.Core.Models.ValueObjects.Seguranca.Senhas;
using System;

namespace Agilis.Domain.Abstractions.Entities.Pessoas
{
    public interface IUsuario
    {
        Guid Id { get; }
        DateTime DataCriacao { get; }
        DateTime DataUltimaAlteracao { get; }
        Email Email { get; }
        string Nome { get; }
        RegraUsuario Regra { get; }
        SenhaMedia Senha { get; }
        string Sobrenome { get; }
    }
}