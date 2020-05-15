using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Flunt.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Agilis.Domain.Abstractions.Services.Seguranca;
using Agilis.WebAPI.ViewModels.Seguranca;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDS.WebAPI.Abstractions.Controllers;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;
using Agilis.WebAPI.Extensions;
using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.WebAPI.ViewModels.Pessoas;
using System.Linq;

namespace Agilis.WebAPI.Controllers.Pessoas
{
    /// <summary>
    /// Manutenção do repositório e serviços da conta, como autenticação, registro, troca de senha, etc.
    /// </summary>    
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : CrudController<UsuarioCadastroViewModel, UsuarioConsultaViewModel, Usuario>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Construtor com parâmetros injetados
        /// </summary>
        /// <param name="usuarioService">Serviço para validação e manipulação da entidade</param>       
        /// <param name="mapper">Automapper</param>
        /// <param name="tokenService">Serviço que gera o token da autenticação do usuário</param>
        /// <param name="httpContextAccessor">Usada para obter o usuário logado</param>        
        public UsuariosController(IUsuarioService usuarioService,
                                 IMapper mapper,
                                 ITokenService tokenService,
                                 IHttpContextAccessor httpContextAccessor)
            : base(usuarioService, mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Realiza alguns procedimentos e validações com a senha antes de chamar o Post da classe base
        /// </summary>
        /// <param name="novoUsuarioViewModel">Dados do novo usuário</param>
        /// <returns>Id do usuário recém cadastrado</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status400BadRequest)]
        public override async Task<ActionResult<Guid>> Post(UsuarioCadastroViewModel novoUsuarioViewModel)
        {
            if (novoUsuarioViewModel.Senha != novoUsuarioViewModel.ConfirmaSenha)
                return CustomBadRequest(nameof(novoUsuarioViewModel.ConfirmaSenha), "As senhas digitadas são diferentes.");

            return await base.Post(novoUsuarioViewModel);
        }

        /// <summary>
        /// Realiza a autenticação do usuário
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns>Dados do usuário + token da autenticação</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioLogadoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody]LoginViewModel loginViewModel)
        {
            var login = _mapper.Map<Login>(loginViewModel);

            var usuario = _usuarioService.Autenticar(login);

            if (_usuarioService.Invalid)
                return BadRequest(_usuarioService.Notifications);

            var token = _tokenService.Gerar(usuario);

            var usuarioLogado = _mapper.Map<UsuarioConsultaViewModel>(usuario);

            return Ok(new UsuarioLogadoViewModel
            {
                Usuario = usuarioLogado,
                Token = token,
                TipoToken = "Bearer"
            });
        }

        /// <summary>
        /// Altera a senha do usuário do repositório
        /// </summary>
        /// <param name="id">Id da entidade que será alterada</param>
        /// <param name="alteraSenhaViewModel">Dados para a alteração da senha</param>
        /// <returns>OK (201)</returns>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notification>), StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> AlterarSenha(Guid id, AlteraSenhaViewModel alteraSenhaViewModel)
        {
            var emailLogado = _httpContextAccessor.ObterEmail();
            var senhaAtual = _mapper.Map<SenhaMedia>(alteraSenhaViewModel.SenhaAtual);
            var novaSenha = _mapper.Map<SenhaMedia>(alteraSenhaViewModel.NovaSenha);
            var confirmaSenha = _mapper.Map<SenhaMedia>(alteraSenhaViewModel.ConfirmaSenha);

            await _usuarioService.AlterarSenha(id, emailLogado, senhaAtual, novaSenha, confirmaSenha);

            if (_usuarioService.Invalid)
                return BadRequest(_usuarioService.Notifications);

            return Ok();
        }

        //TODO: remover exemplo de código
        //[HttpGet]
        //[Route("manager")]//apenas managers podem autenticar
        //[Authorize(Roles = "Admin")]
        //public string Manager() => "Admin";

        /// <summary>
        /// Método abstrato, no qual cada controller implementa a ordenação de forma customizada
        /// </summary>
        /// <param name="lista">Lista a ser ordenada</param>
        /// <returns>Lista já ordenada</returns>
        protected override ICollection<UsuarioConsultaViewModel> Ordenar(ICollection<UsuarioConsultaViewModel> lista)
                => lista.OrderBy(u => u.Nome)
                        .ToList();
    }
}
