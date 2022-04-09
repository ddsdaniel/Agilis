using AutoMapper;
using Microsoft.Extensions.Logging;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.Repositories;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.Seguranca.Enums;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Infra.Seguranca.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Services.Seguranca
{
    public class RefreshTokenAppService : AppService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RefreshTokenAppService> _logger;
        private readonly TokenFactory _tokenFactory;

        public RefreshTokenAppService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ILogger<RefreshTokenAppService> logger,
            TokenFactory tokenFactory
            )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _tokenFactory = tokenFactory;
        }

        public async Task<UsuarioLogadoViewModel> RenovarAsync(RefreshTokenViewModel refreshTokenViewModel)
        {
            var usuario = ExtrairUsuario(refreshTokenViewModel);
            if (Invalido) return null;

            var refreshTokenRepository = _unitOfWork.ObterRepository<RefreshToken>();

            var refreshTokenArmazenado = ValidarToken(refreshTokenViewModel, refreshTokenRepository);
            if (Invalido) return null;

            var novoToken = _tokenFactory.Criar(usuario, TipoToken.Autenticacao);
            var novoRefreshTokenString = _tokenFactory.Criar(usuario, TipoToken.RefreshToken);
            var novoRefreshToken = new RefreshToken(novoRefreshTokenString);
            await PersistirTokens(refreshTokenRepository, novoRefreshToken, refreshTokenArmazenado);

            _logger.LogInformation("Um token foi renovado");

            var usuarioConsulta = _mapper.Map<UsuarioConsultaViewModel>(usuario);
            return new UsuarioLogadoViewModel
            {
                Usuario = usuarioConsulta,
                Token = novoToken,
                TipoToken = "Bearer",
                RefreshToken = novoRefreshTokenString
            };
        }

        private async Task PersistirTokens(
            IRepository<RefreshToken> refreshTokenRepository,
            RefreshToken novoRefreshToken,
            RefreshToken refreshTokenArmazenado)
        {
            await refreshTokenRepository.AdicionarAsync(novoRefreshToken);
            await _unitOfWork.CommitAsync();

            //roda sem esperar a conclusão
            _ = LimparAsync(refreshTokenRepository, refreshTokenArmazenado);
        }

        private async Task LimparAsync(IRepository<RefreshToken> refreshTokenRepository, RefreshToken refreshTokenArmazenado)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10));

                await refreshTokenRepository.ExcluirAsync(refreshTokenArmazenado.Id);
                await refreshTokenRepository.ExcluirAsync(rt => rt.DataCriacao < DateTime.UtcNow.AddDays(-TokenFactory.DIAS_REFRESH_TOKEN));
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                //Evitar write conflict
            }
        }

        private Usuario ExtrairUsuario(RefreshTokenViewModel refreshTokenViewModel)
        {
            var refreshTokenRequest = _mapper.Map<RefreshToken>(refreshTokenViewModel);
            ImportarCriticas(refreshTokenRequest);
            if (Invalido) return null;

            var email = refreshTokenRequest.ObterEmail();
            ImportarCriticas(email);
            if (Invalido) return null;

            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();

            var usuario = usuarioRepository
                .Consultar()
                .FirstOrDefault(u => u.Email.Endereco == email.Endereco);

            if (usuario == null)
            {
                Criticar("Usuário não encontrado.");
                return null;
            }

            return usuario;
        }

        private RefreshToken ValidarToken(RefreshTokenViewModel refreshTokenViewModel, IRepository<RefreshToken> refreshTokenRepository)
        {
            var refreshTokenArmazenado = refreshTokenRepository
                    .Consultar()
                    .FirstOrDefault(rt => rt.Token == refreshTokenViewModel.Token);

            if (refreshTokenArmazenado == null)
            {
                Criticar("Refresh token não encontrado.");
                return null;
            }

            ImportarCriticas(refreshTokenArmazenado);
            if (Invalido) return null;

            return refreshTokenArmazenado;
        }
    }
}
