using DDS.Domain.Core.Abstractions.Services;
using System.Threading.Tasks;
using System;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IProdutoService : IService
    {
        Task<Jornada> AdicionarJornada(Guid produtoId, int posicao, string nome);
        Task ExcluirJornada(Guid produtoId, int posicao);
        
        /// <summary>
        /// Recupera um produto do repositório a partir do seu id
        /// </summary>
        /// <param name="id">Id do produto a ser recuperado</param>
        /// <returns>Task correspondente ao produto consultado, também pode retorna null, caso o produto não seja encontrado</returns>
        Task<Produto> ConsultarPorId(Guid id);

        /// <summary>
        /// Renomeia a produto
        /// </summary>
        /// <param name="timeId">Id do time que contém o produto</param>
        /// <param name="produtoId">Id do produto</param>
        /// <param name="nome">Novo nome do produto</param>
        /// <returns>Task correspondente à renomeação</returns>
        Task Renomear(Guid timeId, Guid produtoId, string nome);

    }
}
