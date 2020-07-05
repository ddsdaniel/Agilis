using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using DDS.Domain.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Abstractions.Repositories.Trabalho
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        ICollection<Produto> ConsultarTodos(IUsuario usuario);
        IQueryable<Guid> ObterTimesDoUsuario(IUsuario usuario);
    }
}
