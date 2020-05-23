using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.Tests.Integracao.Extensions;
using Agilis.WebAPI.ViewModels.Pessoas;
using AutoMapper;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Tests.Services.Pessoas
{
    public class UsuarioServiceTests : Teste
    {
        public UsuarioServiceTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory) 
            : base(customWebApplicationFactory)
        {
            
        }

        [Fact]
        public async void Adicionar_DadosValidos_UsuarioAdicionado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();

            //Act
            await usuarioService.Adicionar(novoUsuario);

            //Assert
            Assert.True(novoUsuario.Valid);
            Assert.True(usuarioService.Valid);

            var usuarioConsulta = await usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.NotNull(usuarioConsulta);
        }

        [Fact]
        public async void AlterarSenha_NovoId_UsuarioNaoEncontrado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();

            //Act
            await usuarioService.AlterarSenha(novoUsuario.Id,
                                               novoUsuario.Email,
                                               novoUsuario.Senha,
                                               new SenhaMedia(novoUsuario.Senha.Conteudo + "abc",Usuario.TAMANHO_MINIMO_SENHA),
                                               new SenhaMedia(novoUsuario.Senha.Conteudo + "abc", Usuario.TAMANHO_MINIMO_SENHA)
                                               );

            //Assert
            Assert.True(novoUsuario.Valid);
            Assert.True(usuarioService.Invalid);

            var usuarioConsulta = await usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.Null(usuarioConsulta);
        }

        [Fact]
        public async void AlterarSenha_SenhasNaoConferem_NaoAlterar()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();
            await usuarioService.Adicionar(novoUsuario);
            var novaSenha = new SenhaMedia(novoUsuario.Senha.Conteudo + "abc", Usuario.TAMANHO_MINIMO_SENHA);
            var confirmaSenha = new SenhaMedia(novoUsuario.Senha.Conteudo + "diferente", Usuario.TAMANHO_MINIMO_SENHA);

            //Act
            await usuarioService.AlterarSenha(novoUsuario.Id,
                                               novoUsuario.Email,
                                               novoUsuario.Senha,
                                               novaSenha,
                                               confirmaSenha
                                               );

            //Assert
            Assert.True(usuarioService.Invalid);

            var usuarioConsulta = await usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.NotNull(usuarioConsulta);
            Assert.NotEqual(usuarioConsulta.Senha.Conteudo, novaSenha.Conteudo);
            Assert.NotEqual(usuarioConsulta.Senha.Conteudo, confirmaSenha.Conteudo);
        }

        [Fact]
        public async void Atualizar_DadosOk_Alterar()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();
            
            await usuarioService.Adicionar(novoUsuario);
            await usuarioService.Commit();

            var novoNome = novoUsuario.Nome + "abc";
            var viewModel = new UsuarioCadastroViewModel
            {
                Id = novoUsuario.Id,
                Nome = novoNome,
                Sobrenome = novoUsuario.Sobrenome,
                Email = novoUsuario.Email.Endereco,
                Senha = novoUsuario.Senha.Conteudo,
                ConfirmaSenha = novoUsuario.Senha.Conteudo,
                Regra = novoUsuario.Regra
            };

            var autoMapper = _customWebApplicationFactory.Services.GetService<IMapper>();
            var usuarioComNomeAtualizado = autoMapper.Map<Usuario>(viewModel);

            //Act
            await usuarioService.Atualizar(usuarioComNomeAtualizado);
            await usuarioService.Commit();

            //Assert
            Assert.True(usuarioComNomeAtualizado.Valid);
            Assert.True(usuarioService.Valid);

            var usuarioConsulta = await usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.NotNull(usuarioConsulta);
            Assert.Equal(novoNome, usuarioConsulta.Nome);       
            
            //usuarioService.dispose
        }

        [Fact]
        public async void Atualizar_EmailDuplicado_NaoAlterar()
        {
            //Arrange
            var novoUsuario1 = UsuarioMock.ObterValido();
            var novoUsuario2 = UsuarioMock.ObterValido();

            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();
            await usuarioService.Adicionar(novoUsuario1);
            await usuarioService.Adicionar(novoUsuario2);
            await usuarioService.Commit();

            var viewModel = new UsuarioCadastroViewModel
            {
                Id = novoUsuario2.Id,
                Nome = novoUsuario2.Nome,
                Sobrenome = novoUsuario2.Sobrenome,
                Email = novoUsuario1.Email.Endereco,
                Senha = novoUsuario2.Senha.Conteudo,
                ConfirmaSenha = novoUsuario2.Senha.Conteudo,
                Regra = novoUsuario2.Regra
            };

            var autoMapper = _customWebApplicationFactory.Services.GetService<IMapper>();
            var usuarioComEmailJaExistente = autoMapper.Map<Usuario>(viewModel);

            //Act
            await usuarioService.Atualizar(usuarioComEmailJaExistente);

            //Assert
            Assert.True(usuarioComEmailJaExistente.Valid);
            Assert.True(usuarioService.Invalid);

            var usuarioConsulta = await usuarioService.ConsultarPorId(novoUsuario2.Id);
            Assert.NotNull(usuarioConsulta);
            Assert.Equal(novoUsuario2.Email.Endereco, usuarioConsulta.Email.Endereco);
        }

        [Fact]
        public async void Autenticar_LoginInvalido_NaoAutenticado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();
            await usuarioService.Adicionar(novoUsuario);
            await usuarioService.Commit();

            var senhaInvalida = new SenhaMedia("123", Usuario.TAMANHO_MINIMO_SENHA);

            var login = new Login(novoUsuario.Email, senhaInvalida);

            //Act
            var usuarioLogado = usuarioService.Autenticar(login);

            //Assert
            Assert.True(login.Invalid);
            Assert.True(usuarioService.Invalid);
            Assert.Null(usuarioLogado);
        }

        [Fact]
        public async void Autenticar_SenhaErrada_NaoAutenticado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            var usuarioService = _customWebApplicationFactory.Services.GetService<IUsuarioService>();
            await usuarioService.Adicionar(novoUsuario);
            await usuarioService.Commit();

            var senhaErrada = new SenhaMedia(novoUsuario.Senha.Conteudo +"-errada", Usuario.TAMANHO_MINIMO_SENHA);

            var login = new Login(novoUsuario.Email, senhaErrada);

            //Act
            var usuarioLogado = usuarioService.Autenticar(login);

            //Assert
            Assert.True(login.Valid);
            Assert.True(usuarioService.Invalid);
            Assert.Null(usuarioLogado);
        }
    }
}
