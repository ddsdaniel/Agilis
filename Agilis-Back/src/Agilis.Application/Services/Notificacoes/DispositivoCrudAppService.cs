using AutoMapper;
using MediatR;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Mensagens;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Services.Notificacoes
{
    public class DispositivoCrudAppService
        : CrudAppService<DispositivoViewModel, DispositivoViewModel, Dispositivo>
    {

        private readonly IRepository<Dispositivo> _dispositivoRepository;
        private readonly IMapper _mapper;

        public DispositivoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, mediator, unitOfWork.ObterRepository<Dispositivo>(), unitOfWork)
        {
            _dispositivoRepository = unitOfWork.ObterRepository<Dispositivo>();
            _mapper = mapper;
        }

        public override async Task<DispositivoViewModel> AdicionarAsync(DispositivoViewModel novaEntidadeViewModel)
        {
            if (novaEntidadeViewModel != null)
            {
                var dispositivo = _dispositivoRepository
                    .Consultar()
                    .FirstOrDefault(d => d.Token == novaEntidadeViewModel.Token);

                if (dispositivo != null)
                {
                    var viewModel = _mapper.Map<DispositivoViewModel>(dispositivo);
                    return viewModel;
                }
            }

            return await base.AdicionarAsync(novaEntidadeViewModel);
        }
    }
}
