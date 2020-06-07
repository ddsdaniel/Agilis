using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
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
                AddNotifications(email);
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

        public async Task<UsuarioVO> AdicionarColaborador(Guid timeId, Email email)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return null;
            }

            if (email.Invalid)
            {
                AddNotifications(email);
                return null;
            }

            var colab = _unitOfWork.UsuarioRepository.ConsultarPorEmail(email);
            if (colab == null)
            {
                AddNotification(nameof(colab), "Usuário não encontrado, revise o e-mail digitado");
                return null;
            }

            var colabVO = new UsuarioVO(colab.Id, colab.NomeCompleto);
            time.AdicionarColaborador(colabVO);
            if (time.Invalid)
            {
                AddNotifications(time);
                return null;
            }
            else
            {
                await Atualizar(time);
                await _unitOfWork.Commit();
                return colabVO;
            }
        }

        public async Task ExcluirColaborador(Guid timeId, Guid colabId)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return;
            }

            var colab = await _unitOfWork.UsuarioRepository.ConsultarPorId(colabId);
            if (colab == null)
            {
                AddNotification(nameof(colab), "Usuário não encontrado, revise o e-mail digitado");
                return;
            }

            time.ExcluirColaborador(colab);
            if (time.Invalid)
            {
                AddNotifications(time);
                return;
            }
            else
            {
                await Atualizar(time);
                await _unitOfWork.Commit();
            }
        }

        public async Task<ProdutoVO> AdicionarProduto(Guid timeId, Produto produto)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return null;
            }

            if (produto.Invalid)
            {
                AddNotifications(produto);
                return null;
            }

            await _unitOfWork.ProdutoRepository.Adicionar(produto);

            var produtoVO = new ProdutoVO(produto.Id, produto.Nome);
            time.AdicionarProduto(produtoVO);
            if (time.Invalid)
            {
                AddNotifications(time);
                return null;
            }
            else
            {
                await Atualizar(time);
                await _unitOfWork.Commit();
                return produtoVO;
            }
        }

        public async Task ExcluirProduto(Guid timeId, Guid prodId)
        {
            var time = await ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return;
            }

            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(prodId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            time.ExcluirProduto(produto);
            if (time.Invalid)
            {
                AddNotifications(time);
                return;
            }
            else
            {
                await _unitOfWork.ProdutoRepository.Excluir(produto.Id);

                await Atualizar(time);
                await _unitOfWork.Commit();
            }
        }
    }
}
