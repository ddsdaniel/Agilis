using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services.Pessoas
{
    public interface IAtorService : ICrudSeguroService<Ator>
    {
        ICollection<Ator> Pesquisar(string filtro, Guid produtoId, IUsuario usuario);
    }
}
