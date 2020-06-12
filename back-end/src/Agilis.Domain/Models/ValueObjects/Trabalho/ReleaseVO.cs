using Flunt.Validations;
using System;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class ReleaseVO : BasicVO
    {
        public int Ordem { get; private set; }
        public ReleaseVO(int ordem, Guid id, string nome) : base(id, nome)
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(ordem, 0, nameof(Ordem), "Ordem não deve ser negativo")
                );

            Ordem = ordem;
        }        
    }
}
