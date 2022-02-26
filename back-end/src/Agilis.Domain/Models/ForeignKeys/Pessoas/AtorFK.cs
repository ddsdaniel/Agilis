using DDS.Domain.Core.Abstractions.Models.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Pessoas
{
    public class AtorFK : ForeignKey
    {

        public AtorFK() : base()
        {

        }

        public AtorFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
