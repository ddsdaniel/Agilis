using DDS.Domain.Core.Abstractions.Models.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class TarefaFK : ForeignKey
    {
        public TarefaFK() : base()
        {

        }

        public TarefaFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
