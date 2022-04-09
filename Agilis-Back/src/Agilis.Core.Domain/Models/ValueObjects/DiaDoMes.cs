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
            Dia = dia;
            Validar();
        }

        private void Validar()
        {
            if (Dia < 1 || Dia > 31)
                Criticar("Dia deve ser entre 1 e 31");
        }
    }
}
