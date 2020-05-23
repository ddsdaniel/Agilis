using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Threading.Tasks;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudService<Produto>
    {
        public Task AdicionarRNF(Guid produtoId, RequisitoNaoFuncional rnf);
    }
}
