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
    public class EpicoService : CrudService<Epico>, IEpicoService
    {

        public EpicoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.EpicoRepository)
        {
        }

        public IEnumerable<Epico> ConsultarTodos(IUsuario usuario)
        {
            var temasId = ObterTemasDoUsuario(usuario).ToList();

            return _unitOfWork.EpicoRepository
                   .AsQueryable()
                   .Where(p => temasId.Contains(p.TemaId))
                   .OrderBy(t => t.Nome)
                   .ToList();
        }

        public override ICollection<Epico> Pesquisar(string filtro)
          => _unitOfWork.EpicoRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        public IEnumerable<Epico> Pesquisar(string filtro, IUsuario usuario)
        {
            var temasId = ObterTemasDoUsuario(usuario);

            return _unitOfWork.EpicoRepository
                    .AsQueryable()
                    .Where(p => temasId.Contains(p.TemaId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public IEnumerable<Epico> Pesquisar(string filtro, Guid temaId, IUsuario usuario)
        {
            var temasId = temaId == Guid.Empty
                ? ObterTemasDoUsuario(usuario)
                : new List<Guid> { temaId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.EpicoRepository
                    .AsQueryable()
                    .Where(t => temasId.Contains(t.TemaId) && t.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        private List<Guid> ObterTemasDoUsuario(IUsuario usuario)
        {
            List<Guid> temasId;
            var timesDoUsuario = _unitOfWork.TimeRepository
              .ObterTimes(usuario)
              .Select(t => t.Id)
              .ToList();

            var produtosId = _unitOfWork.ProdutoRepository
                .AsQueryable()
                .Where(p => timesDoUsuario.Contains(p.TimeId))
                .Select(p => p.Id)
                .ToList();

            temasId = _unitOfWork.TemaRepository
                .AsQueryable()
                .Where(p => produtosId.Contains(p.ProdutoId))
                .Select(p => p.Id)
                .ToList();
            return temasId;
        }
    }
}
