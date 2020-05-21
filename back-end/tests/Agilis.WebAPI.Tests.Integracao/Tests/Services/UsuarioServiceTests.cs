using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.Tests.Integracao.Extensions;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Tests.Services
{
    public class UsuarioServiceTests : Teste
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory) 
            : base(customWebApplicationFactory)
        {
            _usuarioService = customWebApplicationFactory.Services.GetService<IUsuarioService>();
        }

        [Fact]
        public async void Adicionar_DadosValidos_UsuarioAdicionado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();

            //Act
            await _usuarioService.Adicionar(novoUsuario);

            //Assert
            Assert.True(novoUsuario.Valid);
            Assert.True(_usuarioService.Valid);

            var usuarioConsulta = await _usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.NotNull(usuarioConsulta);
        }

        [Fact]
        public async void AlterarSenha_NovoId_UsuarioNaoEncontrado()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();

            //Act
            await _usuarioService.AlterarSenha(novoUsuario.Id,
                                               novoUsuario.Email,
                                               novoUsuario.Senha,
                                               new SenhaMedia(novoUsuario.Senha.Conteudo + "abc",Usuario.TAMANHO_MINIMO_SENHA),
                                               new SenhaMedia(novoUsuario.Senha.Conteudo + "abc", Usuario.TAMANHO_MINIMO_SENHA)
                                               );

            //Assert
            Assert.True(novoUsuario.Valid);
            Assert.True(_usuarioService.Invalid);

            var usuarioConsulta = await _usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.Null(usuarioConsulta);
        }

        [Fact]
        public async void AlterarSenha_SenhasNaoConferem_NaoAlterar()
        {
            //Arrange
            var novoUsuario = UsuarioMock.ObterValido();
            await _usuarioService.Adicionar(novoUsuario);
            var novaSenha = new SenhaMedia(novoUsuario.Senha.Conteudo + "abc", Usuario.TAMANHO_MINIMO_SENHA);
            var confirmaSenha = new SenhaMedia(novoUsuario.Senha.Conteudo + "diferente", Usuario.TAMANHO_MINIMO_SENHA);

            //Act
            await _usuarioService.AlterarSenha(novoUsuario.Id,
                                               novoUsuario.Email,
                                               novoUsuario.Senha,
                                               novaSenha,
                                               confirmaSenha
                                               );

            //Assert
            Assert.True(_usuarioService.Invalid);

            var usuarioConsulta = await _usuarioService.ConsultarPorId(novoUsuario.Id);
            Assert.NotNull(usuarioConsulta);
            Assert.NotEqual(novoUsuario.Senha.Conteudo, novaSenha.Conteudo);
            Assert.NotEqual(novoUsuario.Senha.Conteudo, confirmaSenha.Conteudo);
        }

    }
}
