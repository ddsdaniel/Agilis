using DDS.Domain.Core.Abstractions.Model.ForeignKeys;
using System;

namespace Agilis.Domain.Models.ForeignKeys.Pessoas
{
    public class UsuarioFK : ForeignKey
    {
        public string Email { get; set; }

        public UsuarioFK() : base()
        {

        }

        public UsuarioFK(Guid id, string nome, string email) : base(id, nome)
        {
            Email = email;
        }
    }
}
