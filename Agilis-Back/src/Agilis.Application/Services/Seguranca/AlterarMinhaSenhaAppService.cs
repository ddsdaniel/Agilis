using AutoMapper;
using MediatR;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.Seguranca.Abstractions.Models.Entities;
using Agilis.Infra.Seguranca.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using System.Threading.Tasks;

namespace Agilis.Application.Services.Seguranca
{
    public class AlterarMinhaSenhaAppService : AppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkCatalogo _unitOfWorkCatalogo;
        private readonly IUsuario _usuarioLogado;

        public AlterarMinhaSenhaAppService(
            IMediator mediator, 
            IMapper mapper,
            IUnitOfWorkCatalogo unitOfWorkCatalogo,
            IUsuario usuarioLogado) : base(mediator)
        {
            _mapper = mapper;
            _unitOfWorkCatalogo = unitOfWorkCatalogo;
            _usuarioLogado = usuarioLogado;
        }

        public async Task AlterarAsync(AlterarMinhaSenhaViewModel alterarMinhaSenhaViewModel)
        {
            var alterarMinhaSenha = _mapper.Map<AlterarMinhaSenha>(alterarMinhaSenhaViewModel);
            AddNotifications(alterarMinhaSenha);
            if (Invalid) return;

            var usuarioRepository = _unitOfWorkCatalogo.ObterRepository<Usuario>();
            var usuario = await usuarioRepository.ConsultarPorIdAsync(_usuarioLogado.Id);

            usuario.AlterarSenha(alterarMinhaSenha);
            AddNotifications(usuario);
            if (Invalid) return;

            await usuarioRepository.AlterarAsync(usuario);
            await _unitOfWorkCatalogo.CommitAsync();
        }
    }
}
