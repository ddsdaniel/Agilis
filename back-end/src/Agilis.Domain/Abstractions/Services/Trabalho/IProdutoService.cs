using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudSeguroService<Produto>
    {
        IEnumerable<Produto> Pesquisar(string filtro, Guid timeId, IUsuario usuario);
    }
}
