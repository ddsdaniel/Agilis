using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class ProdutoService : Service, IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Jornada> AdicionarJornada(Guid produtoId, string nome)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return null;
            }

            var jornada = new Jornada(nome);
            if (jornada.Invalid)
            {
                AddNotifications(jornada);
                return null;
            }

            produto.AdicionarJornada(jornada);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return null;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
            return jornada;
        }

        public async Task<Produto> ConsultarPorId(Guid id)
        {
            return await _unitOfWork.ProdutoRepository.ConsultarPorId(id);
        }

        public async Task ExcluirJornada(Guid produtoId, Guid jornadaId)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            produto.ExcluirJornada(jornadaId);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public async Task Renomear(Guid timeId, Guid produtoId, string nome)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrada");
                return;
            }

            var time = await _unitOfWork.TimeRepository.ConsultarPorId(timeId);
            if (time == null)
            {
                AddNotification(nameof(time), "Time não encontrado");
                return;
            }

            produto.Renomear(nome);
            if (produto.Invalid)
            {
                AddNotifications(time);
                return;
            }

            time.RenomearProduto(produtoId, nome);
            if (time.Invalid)
            {
                AddNotifications(time);
                return;
            }

            await _unitOfWork.TimeRepository.Atualizar(time);
            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }
    }
}
