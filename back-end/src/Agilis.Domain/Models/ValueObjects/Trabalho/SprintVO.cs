using System;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class SprintVO : BasicVO
    {
        public SprintVO(Guid id, string nome) 
            : base(id, nome)
        {
        }
    }
}
