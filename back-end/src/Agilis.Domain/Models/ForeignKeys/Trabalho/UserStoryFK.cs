using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Trabalho
{
    public class UserStoryFK : ForeignKey
    {
        public UserStoryFK() : base()
        {

        }

        public UserStoryFK(Guid id, string nome) : base(id, nome)
        {

        }
    }
}
