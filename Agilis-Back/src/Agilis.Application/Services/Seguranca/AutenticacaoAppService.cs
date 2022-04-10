using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Seguranca.Factories;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Core.Domain.Enums;

namespace Agilis.Application.Services.Seguranca
{
    public class AutenticacaoAppService : AppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenFactory _tokenFactory;

        public AutenticacaoAppService(IMapper mapper,
                                      IUnitOfWork unitOfWork,
                                      TokenFactory tokenFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenFactory = tokenFactory;
        }

        public async Task<UsuarioLogadoViewModel> AutenticarAsync(LoginViewModel loginViewModel)
        {
            var email = _mapper.Map<Email>(loginViewModel.Email);
            var senha = _mapper.Map<Senha>(loginViewModel.Senha);

            if (email.Invalido)
            {
                ImportarCriticas(email);
                return null;
            }
            else if (senha.Invalido)
            {
                ImportarCriticas(senha);
                return null;
            }
            else
            {
                var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

                var usuario = usuarioRepository.Consultar().FirstOrDefault(u => u.Email.Endereco == email.Endereco);
                if (usuario == null || usuario.Senha.Conteudo != senha.Conteudo)
                {
                    Criticar("Usuário ou senha incorretos");
                    return null;
                } else if (!usuario.Ativo)
                {
                    Criticar("Usuário inativo.");
                    return null;
                }
                else
                {
                    var token = _tokenFactory.Criar(usuario, TipoToken.Autenticacao);
                    var refreshTokenString = _tokenFactory.Criar(usuario, TipoToken.RefreshToken);

                    var refreskToken = new RefreshToken(refreshTokenString);
                    var refreshTokenRepository = _unitOfWork.ObterRepository<RefreshToken>();
                    await refreshTokenRepository.AdicionarAsync(refreskToken);
                    await _unitOfWork.CommitAsync();

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
