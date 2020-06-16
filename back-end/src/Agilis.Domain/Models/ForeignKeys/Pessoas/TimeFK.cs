using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Pessoas
{
    public class TimeFK: ForeignKey
    {
        public TimeFK() : base()
        {

        }

        public TimeFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
