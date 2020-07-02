using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class ReleaseService : Service, IReleaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReleaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Fase> AdicionarFasePB(Guid releaseId, string nome)
        {
            var release = await _unitOfWork.ReleaseRepository.ConsultarPorId(releaseId);
            if (release == null)
            {
                AddNotification(nameof(release), "Release não encontrada");
                return null;
            }

            var fase = release.ProductBacklog.AdicionarFase(nome);

            if (release.ProductBacklog.Invalid)
            {
                AddNotifications(release.ProductBacklog);
                return null;
            }

            await _unitOfWork.ReleaseRepository.Atualizar(release);
            await _unitOfWork.Commit();
            return fase;
        }

        public async Task<SprintFK> AdicionarSprint(Guid releaseId, string nome)
        {
            var release = await _unitOfWork.ReleaseRepository.ConsultarPorId(releaseId);
            if (release == null)
            {
                AddNotification(nameof(release), "Release não encontrada");
                return null;
            }

            var sprint = new Sprint(nome);
            if (sprint.Invalid)
            {
                AddNotifications(sprint);
                return null;
            }

            await _unitOfWork.SprintRepository.Adicionar(sprint);

            var sprintFK = new SprintFK(sprint.Id, sprint.Nome);
            release.AdicionarSprint(sprintFK);
            if (release.Invalid)
            {
                AddNotifications(release);
                return null;
            }

            await _unitOfWork.ReleaseRepository.Atualizar(release);
            await _unitOfWork.Commit();
            return sprintFK;
        }

        public async Task<Release> ConsultarPorId(Guid id)
        {
            return await _unitOfWork.ReleaseRepository.ConsultarPorId(id);
        }

        public async Task ExcluirSprint(Guid releaseId, Guid sprintId)
        {
            var release = await _unitOfWork.ReleaseRepository.ConsultarPorId(releaseId);
            if (release == null)
            {
                AddNotification(nameof(release), "Release não encontrada");
                return;
            }

            var sprint = await _unitOfWork.SprintRepository.ConsultarPorId(sprintId);
            if (sprint == null)
            {
                AddNotification(nameof(sprint), "Sprint não encontrado");
                return;
            }

            var sprintFK = new SprintFK(sprint.Id, sprint.Nome);
            release.ExcluirSprint(sprintFK);
            if (release.Invalid)
            {
                AddNotifications(release);
                return;
            }

            await _unitOfWork.SprintRepository.Excluir(sprint.Id);
            await _unitOfWork.ReleaseRepository.Atualizar(release);
            await _unitOfWork.Commit();
        }

        public async Task Renomear(Guid timeId, Guid releaseId, string nome)
        {
            var release = await _unitOfWork.ReleaseRepository.ConsultarPorId(releaseId);
            if (release == null)
            {
                AddNotification(nameof(release), "Release não encontrada");
                return;
            }

            var time = await _unitOfWork.TimeRepository.ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return;
            }

            release.Renomear(nome);
            if (release.Invalid)
            {
                AddNotifications(time);
                return;
            }

            await _unitOfWork.ReleaseRepository.Atualizar(release);
            await _unitOfWork.Commit();
        }
    }
}
