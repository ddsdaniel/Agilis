using AutoMapper;
using MediatR;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.Factories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Enums;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Infra.Seguranca.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Services.Seguranca
{
    public class AutenticacaoAppService : AppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkCatalogo _unitOfWork;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly TokenFactory _tokenFactory;

        public AutenticacaoAppService(IMapper mapper,
                                      IMediator mediator,
                                      IUnitOfWorkCatalogo unitOfWork,
                                      IUnitOfWorkFactory unitOfWorkFactory,
                                      TokenFactory tokenFactory)
            : base(mediator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _unitOfWorkFactory = unitOfWorkFactory;
            _tokenFactory = tokenFactory;
        }

        public async Task<UsuarioLogadoViewModel> AutenticarAsync(LoginViewModel loginViewModel)
        {
            var email = _mapper.Map<Email>(loginViewModel.Email);
            var senha = _mapper.Map<Senha>(loginViewModel.Senha);

            if (email.Invalid)
            {
                AddNotifications(email);
                return null;
            }
            else if (senha.Invalid)
            {
                AddNotifications(senha);
                return null;
            }
            else
            {
                var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

                var usuario = usuarioRepository.Consultar().FirstOrDefault(u => u.Email.Endereco == email.Endereco);
                if (usuario == null || usuario.Senha.Conteudo != senha.Conteudo)
                {
                    AddNotification(nameof(loginViewModel), "Usuário ou senha incorretos");
                    return null;
                } else if (!usuario.Ativo)
                {
                    AddNotification(nameof(usuario.Ativo), "Usuário inativo.");
                    return null;
                }
                else
                {
                    var token = _tokenFactory.Criar(usuario, TipoToken.Autenticacao);
                    var refreshTokenString = _tokenFactory.Criar(usuario, TipoToken.RefreshToken);

                    var unitOfWorkInquilino = _unitOfWorkFactory.ObterUnitOfWorkInquilino(usuario.Email);
                    var refreskToken = new RefreshToken(refreshTokenString);
                    var refreshTokenRepository = unitOfWorkInquilino.ObterRepository<RefreshToken>();
                    await refreshTokenRepository.AdicionarAsync(refreskToken);
                    await unitOfWorkInquilino.CommitAsync();

                    var usuarioLogado = _mapper.Map<UsuarioConsultaViewModel>(usuario);

                    return new UsuarioLogadoViewModel
                    {
                        Usuario = usuarioLogado,
                        Token = token,
                        TipoToken = "Bearer",
                        RefreshToken = refreshTokenString
                    };
                }
            }
        }
        
    }
}
