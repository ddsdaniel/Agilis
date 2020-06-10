using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Services.Trabalho
{
    public class SprintService : CrudService<Sprint>, ISprintService
    {

        public SprintService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.SprintRepository)
        {

        }

        public override ICollection<Sprint> Pesquisar(string filtro)
            => _unitOfWork.SprintRepository
                   .AsQueryable()
                   .Where(p => p.Nome.ToLower().Contains(filtro.ToLower()))
                   .OrderBy(p => p.Nome)
                   .ToList();

        #region Regras de Negócio

        public ICollection<Sprint> ConsultarTodos(IUsuario usuario)
        {
            var times = _unitOfWork.TimeRepository.ObterTimes(usuario);
            var produtos = times.SelectMany(t => t.Produtos).ToList();

            return _unitOfWork.SprintRepository
                     .AsQueryable()
                     .Where(s => produtos.Any(p => p.Id == s.Produto.Id))
                     .OrderBy(p => p.Nome)
                     .ToList();
        }

        public ICollection<Sprint> Pesquisar(string filtro, IUsuario usuario)
        {
            var times = _unitOfWork.TimeRepository.ObterTimes(usuario);
            var produtos = times.SelectMany(t => t.Produtos).ToList();

            return _unitOfWork.SprintRepository
                    .AsQueryable()
                    .Where(s => produtos.Any(p => p.Id == s.Produto.Id) &&
                                s.Nome.ToLower().Contains(filtro.ToLower())
                        )
                    .OrderBy(p => p.Nome)
                    .ToList();
        }

        #endregion
    }
}
