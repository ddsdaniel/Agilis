using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Services;
using System;
using System.Linq;
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

        public async Task<Fase> AdicionarFaseJornada(Guid produtoId, int posicao, string nome)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produtoId), "Produto não encontrado");
                return null;
            }

            var jornada = produto.Jornadas.FirstOrDefault(j => j.Posicao == posicao);
            if (jornada == null)
            {
                AddNotification(nameof(posicao), "Jornada não encontrada");
                return null;
            }

            var fase = produto.AdicionarFaseJornada(jornada, nome);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return null;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
            return fase;
        }

        public async Task<Jornada> AdicionarJornada(Guid produtoId, int posicao, string nome)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return null;
            }

            var jornada = new Jornada(posicao, nome);
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

        public async Task ExcluirJornada(Guid produtoId, int posicao)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            produto.ExcluirJornada(posicao);
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

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public async Task RenomearJornada(Guid produtoId, int posicaoJornada, string nome)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            produto.RenomearJornada(posicaoJornada, nome);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }
    }
}
