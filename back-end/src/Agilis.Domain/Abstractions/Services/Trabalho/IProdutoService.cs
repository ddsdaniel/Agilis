using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudSeguroService<Produto>
    {
        IEnumerable<Produto> Pesquisar(string filtro, Guid timeId, IUsuario usuario);
        Task AdicionarTema(Guid produtoId, Tema tema);
        Task AdicionarEpico(Guid produtoId, Guid temaId, Epico epico);
    }
}
