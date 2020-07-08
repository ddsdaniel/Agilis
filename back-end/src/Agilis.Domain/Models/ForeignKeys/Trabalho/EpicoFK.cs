using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class EpicoFK : ForeignKey
    {

        public EpicoFK() : base()
        {

        }

        public EpicoFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
