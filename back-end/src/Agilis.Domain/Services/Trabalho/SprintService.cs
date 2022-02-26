using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using System;
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

        public override IEnumerable<Sprint> Pesquisar(string filtro)
            => _unitOfWork.SprintRepository
                   .AsQueryable()
                   .Where(p => p.Nome.ToLower().Contains(filtro.ToLower()))
                   .OrderBy(p => p.Nome)
                   .ToList();

        #region Regras de Negócio

        //public IEnumerable<Sprint> ConsultarTodos(IUsuario usuario)
        //{
        //    var releaseIds = ObterReleaseIds(usuario);

        //    return _unitOfWork.SprintRepository
        //        .AsQueryable()
        //        .Where(s => releaseIds.Contains(s.Release.Id))
        //        .OrderBy(s => s.Nome)
        //        .ToList();
        //}

        //private Guid[] ObterReleaseIds(IUsuario usuario)
        //{
        //    var timeIds = ObterTimeIds(usuario);

        //    return _unitOfWork.ReleaseRepository
        //                    .AsQueryable()
        //                    .Where(r => timeIds.Contains(r.Time.Id))
        //                    .Select(r => r.Id)
        //                    .ToArray();
        //}

        private Guid[] ObterTimeIds(IUsuario usuario)
        {
            return _unitOfWork.TimeRepository
                            .ObterTimes(usuario)
                            .Select(t => t.Id)
                            .ToArray();
        }

        //public IEnumerable<Sprint> Pesquisar(string filtro, IUsuario usuario)
        //{
        //    var releaseIds = ObterReleaseIds(usuario);

        //    return _unitOfWork.SprintRepository
        //        .AsQueryable()
        //        .Where(s => releaseIds.Contains(s.Release.Id) &&
        //                    s.Nome.ToLower().Contains(filtro.ToLower()))
        //        .OrderBy(s => s.Nome)
        //        .ToList();
        //}

        #endregion
    }
}
