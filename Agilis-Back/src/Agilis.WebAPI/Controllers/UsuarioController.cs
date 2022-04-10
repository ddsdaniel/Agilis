using Agilis.WebAPI.Abstractions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Agilis.Application.Services.Seguranca;
using AutoMapper;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.WebAPI.Extensions;
using System;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Application.ViewModels.Mensagens;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects.Seguranca;
using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.WebAPI.Controllers
{
    public class UsuarioController : CrudController<UsuarioCadastroViewModel, UsuarioConsultaViewModel, Usuario>
    {
        private readonly UsuarioCrudAppService _usuarioCrudAppService;

        public UsuarioController(UsuarioCrudAppService crudAppService) : base(crudAppService)
        {
            _usuarioCrudAppService = crudAppService;
        }


        [AllowAnonymous]
        public override async Task<ActionResult> Post(UsuarioCadastroViewModel novaEntidadeViewModel)
        {
            return await base.Post(novaEntidadeViewModel);
        }

        [HttpGet]
        public ActionResult<UsuarioCadastroViewModel[]> Consultar()
        {
            var usuariosViewModel = _usuarioCrudAppService.ConsultarTodos();

            if (_usuarioCrudAppService.Invalido)
                return CustomBadRequest(_usuarioCrudAppService);

            return Ok(usuariosViewModel);
        }

        /// <summary>
        /// Realiza a autenticação do usuário
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns>Dados do usuário + token da autenticação</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioLogadoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioLogadoViewModel>> Login(
            [FromBody] LoginViewModel loginViewModel,
            [FromServices] AutenticacaoAppService usuarioAutenticacaoAppService)
        {
            var usuarioLogado = await usuarioAutenticacaoAppService.AutenticarAsync(loginViewModel);

            if (usuarioAutenticacaoAppService.Invalido)
                return CustomBadRequest(usuarioAutenticacaoAppService);

            return Ok(usuarioLogado);
        }

        [HttpDelete("conta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExcluirConta([FromServices] IUsuario usuarioLogado)
        {
            await _usuarioCrudAppService.ExcluirAsync(usuarioLogado.Id);

            if (_usuarioCrudAppService.Invalido)
                return CustomBadRequest(_usuarioCrudAppService);

            return Ok();
        }

        [HttpPut("senha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult> AlterarMinhaSenha(
            [FromServices] AlterarMinhaSenhaAppService alterarMinhaSenhaAppService,
            AlterarMinhaSenhaViewModel alterarMinhaSenhaViewModel
            )
        {
            await alterarMinhaSenhaAppService.AlterarAsync(alterarMinhaSenhaViewModel);

            if (alterarMinhaSenhaAppService.Invalido)
                return CustomBadRequest(alterarMinhaSenhaAppService);

            return Ok();
        }

        [HttpPost("esqueci-minha-senha")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public ActionResult RequisitarEsqueciMinhaSenha(
            [FromBody] EmailViewModel emailViewModel,
            [FromServices] EsqueciMinhaSenhaAppService esqueciMinhaSenhaAppService,
            [FromServices] IMapper mapper
            )
        {
            var email = mapper.Map<Email>(emailViewModel);

            esqueciMinhaSenhaAppService.Requisitar(email, Request.GetFrontUrl());

            if (esqueciMinhaSenhaAppService.Invalido)
                return CustomBadRequest(esqueciMinhaSenhaAppService);

            return Ok();
        }

        [HttpPut("redefinir-minha-senha/{email}/{chave:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioLogadoViewModel>> RequisitarEsqueciMinhaSenha(
            [FromRoute] string email,
            [FromRoute] Guid chave,
            [FromBody] RedefinicaoSenhaViewModel redefinicaoSenhaViewModel,
            [FromServices] EsqueciMinhaSenhaAppService esqueciMinhaSenhaAppService,
            [FromServices] IMapper mapper
            )
        {
            var redefinicaoSenha = mapper.Map<RedefinicaoSenha>(redefinicaoSenhaViewModel);

            var usuarioLogadoViewModel = await esqueciMinhaSenhaAppService.RedefinirAsync(
                new Email(email),
                chave,
                redefinicaoSenha
                );

            if (esqueciMinhaSenhaAppService.Invalido)
                return CustomBadRequest(esqueciMinhaSenhaAppService);

            return Ok(usuarioLogadoViewModel);
        }
    }
}
