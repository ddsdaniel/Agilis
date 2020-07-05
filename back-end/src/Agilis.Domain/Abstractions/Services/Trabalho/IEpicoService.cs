using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IEpicoService : ICrudSeguroService<Epico>
    {
        IEnumerable<Epico> Pesquisar(string filtro, Guid temaId, IUsuario usuario);
    }
}
