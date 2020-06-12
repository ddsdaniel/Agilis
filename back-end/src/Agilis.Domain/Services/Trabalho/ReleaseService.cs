using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class ReleaseService : CrudService<Release>, IReleaseService
    {

        public ReleaseService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ReleaseRepository)
        {

        }

        public override ICollection<Release> Pesquisar(string filtro)
            => _unitOfWork.ReleaseRepository
                   .AsQueryable()
                   .Where(p => p.Nome.ToLower().Contains(filtro.ToLower()))
                   .OrderBy(p => p.Nome)
                   .ToList();

        public ICollection<Release> ConsultarTodos(IUsuario usuario)
        {
            var timeIds = ObterTimeIds(usuario);

            return _unitOfWork.ReleaseRepository
                .AsQueryable()
                .Where(p => timeIds.Contains(p.Time.Id))
                .OrderBy(p => p.Nome)
                .ToList();
        }

        private Guid[] ObterTimeIds(IUsuario usuario)
        {
            return _unitOfWork.TimeRepository
                            .ObterTimes(usuario)
                            .Select(t => t.Id)
                            .ToArray();
        }

        public ICollection<Release> Pesquisar(string filtro, IUsuario usuario)
        {
            var timeIds = ObterTimeIds(usuario);

            return _unitOfWork.ReleaseRepository
                    .AsQueryable()
                    .Where(p => timeIds.Contains(p.Time.Id) &&
                                p.Nome.ToLower().Contains(filtro.ToLower())
                        )
                    .OrderBy(p => p.Nome)
                    .ToList();
        }
    }
}
