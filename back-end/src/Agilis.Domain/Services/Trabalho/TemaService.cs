using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class TemaService : CrudService<Tema>, ITemaService
    {

        public TemaService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.TemaRepository)
        {
        }

        public ICollection<Tema> ConsultarTodos(IUsuario usuario)
        {
            var timesId = ObterTimesId(usuario).ToList();

            return _unitOfWork.TemaRepository
                   .AsQueryable()
                   .Where(p => timesId.Contains(p.ProdutoId))
                   .OrderBy(t => t.Nome)
                   .ToList();
        }

        public override ICollection<Tema> Pesquisar(string filtro)
          => _unitOfWork.TemaRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        private IQueryable<Guid> ObterTimesId(IUsuario usuario)
        {
            return _unitOfWork.TimeRepository
                .ObterTimes(usuario)
                .Select(t => t.Id);
        }

        public ICollection<Tema> Pesquisar(string filtro, IUsuario usuario)
        {
            var timesId = ObterTimesId(usuario);

            return _unitOfWork.TemaRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.ProdutoId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public ICollection<Tema> Pesquisar(string filtro, Guid produtoId, IUsuario usuario)
        {
            var produtosId = new List<Guid> { produtoId };

            if (produtoId == Guid.Empty)
            {
                var timesDoUsuario = _unitOfWork.TimeRepository
                    .ObterTimes(usuario)
                    .Select(t => t.Id)
                    .ToList();

                produtosId = _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesDoUsuario.Contains(p.TimeId))
                    .Select(p => p.Id)
                    .ToList();
            }

            if (filtro == null)
                filtro = "";

            return _unitOfWork.TemaRepository
                    .AsQueryable()
                    .Where(t => produtosId.Contains(t.ProdutoId) && t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

    }
}
