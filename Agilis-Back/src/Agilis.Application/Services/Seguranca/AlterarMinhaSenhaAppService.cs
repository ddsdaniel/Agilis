using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using System.Threading.Tasks;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Application.Services.Seguranca
{
    public class AlterarMinhaSenhaAppService : AppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuario _usuarioLogado;

        public AlterarMinhaSenhaAppService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUsuario usuarioLogado) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _usuarioLogado = usuarioLogado;
        }

        public async Task AlterarAsync(AlterarMinhaSenhaViewModel alterarMinhaSenhaViewModel)
        {
            var alterarMinhaSenha = _mapper.Map<AlterarMinhaSenha>(alterarMinhaSenhaViewModel);
            ImportarCriticas(alterarMinhaSenha);
            if (Invalido) return;

            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();
            var usuario = await usuarioRepository.ConsultarPorIdAsync(_usuarioLogado.Id);

            usuario.AlterarSenha(alterarMinhaSenha);
            ImportarCriticas(usuario);
            if (Invalido) return;

            await usuarioRepository.AlterarAsync(usuario);
            await _unitOfWork.CommitAsync();
        }
    }
}
