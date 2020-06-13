using DDS.Domain.Core.Abstractions.Services;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Threading.Tasks;
using System;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using System.Collections.Generic;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : ICrudService<Produto>
    {
        ICollection<Produto> ConsultarTodos(IUsuario usuario);
        ICollection<Produto> Pesquisar(string filtro, IUsuario usuario);

        public Task AdicionarRNF(Guid produtoId, RequisitoNaoFuncional rnf);
        public Task RemoverRNF(Guid produtoId, int numero);
        public Task AtualizarDescricaoRNF(Guid produtoId, int numeroRnf, string descricao);
        public Task AtualizarTipoRNF(Guid produtoId, int numeroRnf, TipoRequisitoNaoFuncional tipo);        
    }
}
