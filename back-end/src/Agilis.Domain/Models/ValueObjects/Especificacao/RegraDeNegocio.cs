using Agilis.Domain.Models.Entities.Pessoas;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Especificacao
{
    public class RegraDeNegocio : ValueObject<RegraDeNegocio>
    {
        public int Numero { get; private set; }
        public string Codigo => $"RN{Numero}";
        public string Descricao { get; private set; }
        public Usuario Autor { get; private set; }

        public RegraDeNegocio(int numero, string descricao, Usuario autor)
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
