using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Domain.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Repositories.Trabalho
{
    public interface IEpicoRepository : IRepository<Epico>
    {
        IEnumerable<Epico> ConsultarTodos(IEnumerable<Guid> temasId);
    }
}
