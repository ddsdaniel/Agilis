using DDS.Domain.Core.Abstractions.Models.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class SprintFK : ForeignKey
    {
        public SprintFK() : base()
        {

        }

        public SprintFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
