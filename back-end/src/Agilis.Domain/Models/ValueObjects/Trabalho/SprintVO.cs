using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class SprintVO : BasicVO
    {
        public int Numero { get; private set; }

        public SprintVO(Guid id, string nome, int numero) 
            : base(id, nome)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(numero, 0, nameof(Numero), "Número deve ser maior ou igual a zero")
                );

            Numero = numero;
        }
    }
}
