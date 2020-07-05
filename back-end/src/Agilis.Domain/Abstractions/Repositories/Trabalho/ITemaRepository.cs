using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Domain.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Repositories.Trabalho
{
    public interface ITemaRepository : IRepository<Tema>
    {
        IEnumerable<Tema> ConsultarTodos(IEnumerable<Guid> produtosId);
    }
}
