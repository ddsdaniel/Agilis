using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class ReleaseFK : ForeignKey
    {
        public ReleaseFK() : base()
        {

        }

        public ReleaseFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
