using Flunt.Validations;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class DiaDoMes : ValueObject<DiaDoMes>
    {
        public int Dia { get; private set; }

        [ExcludeFromCodeCoverage] protected DiaDoMes() { }

        public DiaDoMes(int dia)
        {
            AddNotifications(new Contract()
                .IsBetween(dia, 1, 31, nameof(Dia), "Dia deve ser entre 1 e 31")
                );

            Dia = dia;
        }
    }
}
