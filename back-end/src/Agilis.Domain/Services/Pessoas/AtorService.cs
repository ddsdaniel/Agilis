using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Pessoas
{
    public class AtorService : CrudService<Ator>, IAtorService
    {

        public AtorService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.AtorRepository)
        {
        }

        public IEnumerable<Ator> ConsultarTodos(IUsuario usuario)
        {
            var produtosId = ObterProdutosDoUsuario(usuario).ToList();

            return _unitOfWork.AtorRepository
                   .AsQueryable()
                   .Where(p => produtosId.Contains(p.ProdutoId))
                   .OrderBy(p => p.Nome)
                   .ToList();
        }

        public override ICollection<Ator> Pesquisar(string filtro)
          => _unitOfWork.AtorRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        public IEnumerable<Ator> Pesquisar(string filtro, IUsuario usuario)
        {
            var produtosId = ObterProdutosDoUsuario(usuario);

            return _unitOfWork.AtorRepository
                    .AsQueryable()
                    .Where(p => produtosId.Contains(p.ProdutoId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public IEnumerable<Ator> Pesquisar(string filtro, Guid produtoId, IUsuario usuario)
        {
            var produtosId = produtoId == Guid.Empty
                ? ObterProdutosDoUsuario(usuario)
                : new List<Guid> { produtoId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.AtorRepository
                    .AsQueryable()
                    .Where(t => produtosId.Contains(t.ProdutoId) && t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        private List<Guid> ObterProdutosDoUsuario(IUsuario usuario)
        {
            List<Guid> produtosId;
            var timesDoUsuario = _unitOfWork.TimeRepository
           .ObterTimes(usuario)
           .Select(t => t.Id)
           .ToList();

            produtosId = _unitOfWork.ProdutoRepository
                .AsQueryable()
                .Where(p => timesDoUsuario.Contains(p.TimeId))
                .Select(p => p.Id)
                .ToList();
            return produtosId;
        }

        public override async Task Adicionar(Ator ator)
        {
            await base.Adicionar(ator);
            if (Valid)
            {
                var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(ator.ProdutoId);
                var atorFK = new AtorFK(ator.Id, ator.Nome);
                produto.AdicionarAtor(atorFK);
                if (produto.Valid)
                    await _unitOfWork.ProdutoRepository.Atualizar(produto);
                else
                    AddNotifications(produto);
            }
        }
    }
}
