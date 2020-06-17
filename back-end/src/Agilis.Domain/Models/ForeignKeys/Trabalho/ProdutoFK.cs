using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class ProdutoFK : ForeignKey
    {
        public ProdutoFK() : base()
        {

        }

        public ProdutoFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
