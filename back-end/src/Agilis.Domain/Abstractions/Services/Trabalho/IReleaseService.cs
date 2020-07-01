using DDS.Domain.Core.Abstractions.Services;
using System.Threading.Tasks;
using System;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;

namespace Agilis.Domain.Abstractions.Services.Trabalho
{
    public interface IReleaseService : IService
    {
        Task<SprintFK> AdicionarSprint(Guid releaseId, string nome);
        Task ExcluirSprint(Guid releaseId, Guid sprintId);
        
        /// <summary>
        /// Recupera uma release do repositório a partir do seu id
        /// </summary>
        /// <param name="id">Id da release a ser recuperada</param>
        /// <returns>Task correspondente à release consultada, também pode retorna null, caso a release não seja encontrada</returns>
        Task<Release> ConsultarPorId(Guid id);

        /// <summary>
        /// Renomeia a release
        /// </summary>
        /// <param name="timeId">Id do time que contém a release</param>
        /// <param name="releaseId">Id da release</param>
        /// <param name="nome">Novo nome da release</param>
        /// <returns>Task correspondente à renomeação</returns>
        Task Renomear(Guid timeId, Guid releaseId, string nome);

        /// <summary>
        /// Adiciona uma fase ao product backlog
        /// </summary>
        /// <param name="releaseId">Id da release</param>
        /// <param name="nome">Nome da fase</param>
        /// <returns>Retorna a fase adicionada</returns>
        Task<Fase> AdicionarFasePB(Guid releaseId, string nome);
    }
}
