using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class TemaService : CrudService<Tema>, ITemaService
    {

        public TemaService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.TemaRepository)
        {
        }

        public IEnumerable<Tema> ConsultarTodos(IUsuario usuario)
        {
            var produtosId = ObterProdutosDoUsuario(usuario).ToList();

            return _unitOfWork.TemaRepository
                   .AsQueryable()
                   .Where(p => produtosId.Contains(p.ProdutoId))
                   .OrderBy(p => p.Nome)
                   .ToList();
        }

        public override ICollection<Tema> Pesquisar(string filtro)
          => _unitOfWork.TemaRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        public IEnumerable<Tema> Pesquisar(string filtro, IUsuario usuario)
        {
            var produtosId = ObterProdutosDoUsuario(usuario);

            return _unitOfWork.TemaRepository
                    .AsQueryable()
                    .Where(p => produtosId.Contains(p.ProdutoId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public ICollection<Tema> Pesquisar(string filtro, Guid produtoId, IUsuario usuario)
        {
            var produtosId = produtoId == Guid.Empty
                ? ObterProdutosDoUsuario(usuario)
                : new List<Guid> { produtoId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.TemaRepository
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

        public override async Task Adicionar(Tema tema)
        {
            await base.Adicionar(tema);
            if (Valid)
            {
                var produto = await _unitOfWork.ProdutoRepository.ConsultarPorId(tema.ProdutoId);
                var temaFK = new TemaFK(tema.Id, tema.Nome);
                produto.AdicionarTema(temaFK);
                if (produto.Valid)
                    await _unitOfWork.ProdutoRepository.Atualizar(produto);
                else
                    AddNotifications(produto);
            }
        }
    }
}
