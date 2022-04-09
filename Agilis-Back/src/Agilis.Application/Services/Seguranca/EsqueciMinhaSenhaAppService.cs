using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Emails.Abstractions.Services;
using Agilis.Infra.Seguranca.Enums;
using Agilis.Infra.Seguranca.Factories;
using Agilis.Infra.Seguranca.Models.Entities;
using Agilis.Infra.Seguranca.Models.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;

namespace Agilis.Application.Services.Seguranca
{
    public class EsqueciMinhaSenhaAppService : AppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly TokenFactory _tokenFactory;

        public EsqueciMinhaSenhaAppService(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IEmailService emailService,
            IMapper mapper,
            TokenFactory tokenFactory
            ) 
            : base(mediator)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _emailService = emailService;
            _mapper = mapper;
            _tokenFactory = tokenFactory;
        }

        public void Requisitar(Email email, string frontUrl)
        {
            AddNotifications(email);
            if (Invalid) return;

            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();
            var usuario = usuarioRepository
                .Consultar()
                .FirstOrDefault(u => u.Email.Endereco == email.Endereco);

            if (usuario == null)
            {
                AddNotification(nameof(email), "Usuário não encontrado");
                return;
            }

            var chave = Guid.NewGuid();
            _memoryCache.Set($"EsqueciMinhaSenha.{usuario.Email.Endereco}", chave, DateTime.Now.AddMinutes(10));
            
            const string ASSUNTO = "Redefinição de Senha";
            var linkRedefinicao = $"<a href=\"{frontUrl}#/redefinir-minha-senha/{usuario.Email}/{chave}\">aqui</a>";
            var mensagem = $"Foi solicitada uma redefinição de senha para a sua conta no Agilis. Caso queira prosseguir clique {linkRedefinicao}.";
            _emailService.Enviar(usuario.Email, ASSUNTO, mensagem);

            AddNotifications(_emailService.Notifications);
        }

        public async Task<UsuarioLogadoViewModel> RedefinirAsync(
            Email email,
            Guid chave,
            RedefinicaoSenha redefinicaoSenha)
        {
            AddNotifications(email);
            if (Invalid) return null;

            var usuarioRepository = _unitOfWork.ObterRepository<Usuario>();
            var usuario = usuarioRepository
                .Consultar()
                .FirstOrDefault(u => u.Email.Endereco == email.Endereco);

            if (usuario == null)
            {
                AddNotification(nameof(email), "Usuário não encontrado");
                return null;
            }

            var chaveEsperada = _memoryCache.Get<Guid>($"EsqueciMinhaSenha.{usuario.Email.Endereco}");
            if (chaveEsperada == Guid.Empty)
            {
                AddNotification(nameof(chave), "Tempo esgotado, tente novamente.");
                return null;
            }

            if (chave != chaveEsperada)
            {
                AddNotification(nameof(chave), "Chave de redefinição incorreta.");
                return null;
            }

            usuario.RedefinirSenha(redefinicaoSenha);
            AddNotifications(usuario);
            if (Invalid) return null;
            
            await usuarioRepository.AlterarAsync(usuario);
            await _unitOfWork.CommitAsync();

            _memoryCache.Remove($"EsqueciMinhaSenha.{usuario.Email.Endereco}");

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
