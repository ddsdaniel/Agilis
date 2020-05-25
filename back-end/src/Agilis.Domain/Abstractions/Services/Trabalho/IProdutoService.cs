using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Threading.Tasks;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Especificacao;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudService<Produto>
    {
        public Task AdicionarRNF(Guid produtoId, RequisitoNaoFuncional rnf);
        public Task RemoverRNF(Guid produtoId, int numero);
        public Task AtualizarDescricaoRNF(Guid produtoId, int numeroRnf, string descricao);
        public Task AtualizarTipoRNF(Guid produtoId, int numeroRnf, TipoRequisitoNaoFuncional tipo);

        public Task AdicionarModulo(Guid produtoId, Modulo modulo);
        public Task RemoverModulo(Guid produtoId, int numero);
        public Task AtualizarNomeModulo(Guid produtoId, int numero, string nome);
    }
}
