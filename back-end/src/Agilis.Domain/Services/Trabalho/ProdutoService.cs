using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class ProdutoService : CrudService<Produto>, IProdutoService
    {

        public ProdutoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProdutoRepository)
        {

        }

        public override ICollection<Produto> Pesquisar(string filtro)
            => _unitOfWork.ProdutoRepository
                   .AsQueryable()
                   .Where(p => p.Nome.ToLower().Contains(filtro.ToLower()))
                   .OrderBy(p => p.Nome)
                   .ToList();

        #region Regras de Negócio

        public async Task AdicionarRNF(Guid produtoId, RequisitoNaoFuncional rnf)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produtoId), "Produto não encontrado.");
                return;
            }

            produto.AdicionarRNF(rnf);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public async Task RemoverRNF(Guid produtoId, int numero)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produtoId), "Produto não encontrado.");
                return;
            }

            produto.RemoverRNF(numero);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public async Task AtualizarDescricaoRNF(Guid produtoId, int numeroRnf, string descricao)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produtoId), "Produto não encontrado.");
                return;
            }

            produto.AtualizarDescricaoRnf(numeroRnf, descricao);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public async Task AtualizarTipoRNF(Guid produtoId, int numeroRnf, TipoRequisitoNaoFuncional tipo)
        {
            var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produtoId), "Produto não encontrado.");
                return;
            }

            produto.AtualizarTipoRnf(numeroRnf, tipo);
            if (produto.Invalid)
            {
                AddNotifications(produto);
                return;
            }

            await _unitOfWork.ProdutoRepository.Atualizar(produto);
            await _unitOfWork.Commit();
        }

        public ICollection<Produto> ConsultarTodos(IUsuario usuario)
        {
            var times = _unitOfWork.TimeRepository.ObterTimes(usuario);

            return _unitOfWork.ProdutoRepository
                     .AsQueryable()
                     .Where(p => times.Any(t => t.Id == p.Time.Id))
                     .OrderBy(p => p.Nome)
                     .ToList();
        }

        public ICollection<Produto> Pesquisar(string filtro, IUsuario usuario)
        {
            var times = _unitOfWork.TimeRepository.ObterTimes(usuario);

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => times.Any(t => t.Id == p.Time.Id) &&
                                p.Nome.ToLower().Contains(filtro.ToLower())
                        )
                    .OrderBy(p => p.Nome)
                    .ToList();
        }

        #endregion
    }
}
