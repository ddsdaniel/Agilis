using System;

namespace Agilis.Domain.Models.ValueObjects.Pessoas
{
    public class UsuarioVO : BasicVO
    {
        public UsuarioVO(Guid id, string nome) : base(id, nome)
        {
        }
    }
}
