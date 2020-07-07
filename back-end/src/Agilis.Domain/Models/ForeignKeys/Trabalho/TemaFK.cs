using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class TemaFK : ForeignKey
    {

        public TemaFK() : base()
        {

        }

        public TemaFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
