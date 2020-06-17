using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
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
