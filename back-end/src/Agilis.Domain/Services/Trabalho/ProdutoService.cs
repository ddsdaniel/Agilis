using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
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

        public IEnumerable<Produto> ConsultarTodos(IUsuario usuario)
        {
            var timesId = _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToList();
            return _unitOfWork.ProdutoRepository.ConsultarTodos(timesId);;
        }

        public override ICollection<Produto> Pesquisar(string filtro)
          => _unitOfWork.ProdutoRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        public IEnumerable<Produto> Pesquisar(string filtro, IUsuario usuario)
        {
            var timesId = _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToList();

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public IEnumerable<Produto> Pesquisar(string filtro, Guid timeId, IUsuario usuario)
        {
            var timesId = timeId == Guid.Empty
                ? _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToArray()
                : new Guid[] { timeId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public override async Task Adicionar(Produto produto)
        {
            //TODO: implementar Renomear de todas as FKs
            await base.Adicionar(produto);
            if (Valid)
            {
                var time = await _unitOfWork.TimeRepository.ConsultarPorId(produto.TimeId);
                var produtoFK = new ProdutoFK(produto.Id, produto.Nome);
                time.AdicionarProduto(produtoFK);
                if (time.Valid)
                    await _unitOfWork.TimeRepository.Atualizar(time);
                else
                    AddNotifications(time);
            }
        }

    }
}
