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
    }
}
