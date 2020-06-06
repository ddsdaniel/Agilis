using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using DDS.Domain.Core.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Pessoas
{
    public class TimeService : CrudService<Time>, ITimeService
    {

        public TimeService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.TimeRepository)
        {

        }

        public override ICollection<Time> Pesquisar(string filtro)
             => _unitOfWork.TimeRepository
                    .AsQueryable()
                    .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();

        public ICollection<Time> Pesquisar(string filtro, IUsuario usuario)
            => _unitOfWork.TimeRepository
                    .ObterTimes(usuario)
                    .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();

        public override async Task Excluir(Guid id)
        {
            var time = await ConsultarPorId(id);

            if (time != null && time.Escopo == EscopoTime.Pessoal)
            {
                AddNotification(nameof(time.Escopo), "O time pessoal não pode ser excluído");
                return;
            }

            await base.Excluir(id);
        }

        public override async Task Atualizar(Time time)
        {
            if (time.Escopo == EscopoTime.Pessoal)
            {
                AddNotification(nameof(time.Escopo), "O time pessoal não pode ser alterado");
                return;
            }

            await base.Atualizar(time);
        }

        public override async Task Adicionar(Time time)
        {
            if (time.Escopo == EscopoTime.Pessoal)
            {
                AddNotification(nameof(time.Escopo), "Cada usuário pode ter apenas um time pessoal");
                return;
            }

            await base.Adicionar(time);
        }

        public ICollection<Time> ConsultarTodos(IUsuario usuario)
           => _unitOfWork.TimeRepository
                    .ObterTimes(usuario)
                    .OrderBy(t => t.Nome)
                    .ToList();

        public async Task<UsuarioVO> AdicionarAdmin(Guid timeId, Email email)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return null;
            }

            if (email.Invalid)
            {
                AddNotification(nameof(email), "E-mail inválido");
                return null;
            }

            var admin = _unitOfWork.UsuarioRepository.ConsultarPorEmail(email);
            if (admin == null)
            {
                AddNotification(nameof(admin), "Usuário não encontrado, revise o e-mail digitado");
                return null;
            }

            var adminVO = new UsuarioVO(admin.Id, admin.NomeCompleto);
            time.AdicionarAdmin(adminVO);
            if (time.Invalid)
            {
                AddNotifications(time);
                return null;
            }
            else
            {
                await Atualizar(time);
                await _unitOfWork.Commit();
                return adminVO;
            }
        }

        public async Task ExcluirAdmin(Guid timeId, Guid adminId)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return;
            }

            var admin = await _unitOfWork.UsuarioRepository.ConsultarPorId(adminId);
            if (admin == null)
            {
                AddNotification(nameof(admin), "Usuário não encontrado, revise o e-mail digitado");
                return;
            }

            time.ExcluirAdmin(admin);
            if (time.Invalid)
            {
                AddNotifications(time);
                return ;
            }
            else
            {
                await Atualizar(time);
                await _unitOfWork.Commit();
            }
        }
    }
}
