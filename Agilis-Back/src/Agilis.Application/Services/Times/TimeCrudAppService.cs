using AutoMapper;
using MediatR;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Application.ViewModels.Times;

namespace Agilis.Application.Services.Notificacoes
{
    public class TimeCrudAppService
        : CrudAppService<TimeViewModel, TimeViewModel, Time>
    {

        private readonly IRepository<Time> _timeRepository;
        private readonly IMapper _mapper;

        public TimeCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, mediator, unitOfWork.ObterRepository<Time>(), unitOfWork)
        {
            _timeRepository = unitOfWork.ObterRepository<Time>();
            _mapper = mapper;
        }

        public override async Task<TimeViewModel> AdicionarAsync(TimeViewModel novaEntidadeViewModel)
        {
            if (novaEntidadeViewModel != null)
            {
                var time = _timeRepository
                    .Consultar()
                    .FirstOrDefault(d => d.Nome == novaEntidadeViewModel.Nome);

                if (time != null)
                {
                    var viewModel = _mapper.Map<TimeViewModel>(time);
                    return viewModel;
                }
            }

            return await base.AdicionarAsync(novaEntidadeViewModel);
        }
    }
}
