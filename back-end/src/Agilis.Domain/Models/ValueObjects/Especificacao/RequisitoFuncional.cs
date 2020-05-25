using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Especificacao
{
    /// <summary>
    /// Representam as funcionalidades e recursos que o módulo deve conter
    /// </summary>
    public class RequisitoFuncional : ValueObject<RequisitoFuncional>
    {
        public int Numero { get; private set; }
        public string Codigo => $"RF{Numero}";
        public string Descricao { get; private set; }
        public Usuario Autor { get; private set; }

        public RequisitoFuncional(int numero, string descricao, Usuario autor)
        {
            AddNotifications(new Contract()
                .IsGreaterThan(numero, 0, nameof(Numero), "Número deve ser maior que zero")
                .IsNotNullOrEmpty(descricao, nameof(Descricao), "Descrição não deve ser nula ou vazia")
                .IsNotNull(autor, nameof(Autor), "Autor não deve ser nulo")
                .IfNotNull(autor, c => c.Join(autor))
                );

            Numero = numero;
            Descricao = descricao;
            Autor = autor;
        }

        public override string ToString() => $"{Codigo}: {Descricao}";

    }
}
