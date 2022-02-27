using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Enums;
using Agilis.Test.Integration.Config;
using Agilis.Test.Integration.WebAPI.Constantes;
using Agilis.Test.Integration.WebAPI.Extensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Agilis.Test.Integration.WebAPI.Controllers
{
    [Collection(nameof(IntegrationWebTestFixtureCollection))]
    public class UsuarioControllerTest
    {
        private readonly IntegrationTestFixture _integrationTestFixture;

        public UsuarioControllerTest(
            IntegrationTestFixture integrationTestFixture
            )
        {
            _integrationTestFixture = integrationTestFixture;
        }

        //[Fact]
        //public async Task Post_UsuarioValido_IsSuccessStatusCode()
        //{
        //    //Arrange
        //    var novoUsuario = new UsuarioCadastroViewModel
        //    {
        //        Nome = "Agilis",
        //        Sobrenome = "DDS Sistemas",
        //        Ativo = true,
        //        Email = "sistema.Agilis@gmail.com.br",
        //        LicencaCompleta = false,
        //        Senha = "123",
        //        ConfirmaSenha = "123",
        //        Regra = RegraUsuario.Usuario,
        //        Id = Guid.Empty
        //    };

        //    //Act
        //    var response = await _integrationTestFixture.HttpClient.PostViewModelAsync(RotasConst.Usuario, novoUsuario);
        //    var usuarioCadastrado = await response.Content.ReadAsViewModelAsync<UsuarioConsultaViewModel>();

        //    //Assert
        //    Assert.True(response.IsSuccessStatusCode);
        //    Assert.NotNull(usuarioCadastrado);
        //    Assert.Equal(novoUsuario.Nome, usuarioCadastrado.Nome);
        //    Assert.Equal(novoUsuario.Sobrenome, usuarioCadastrado.Sobrenome);
        //    Assert.Equal(novoUsuario.Email, usuarioCadastrado.Email);
        //    Assert.Equal(novoUsuario.Regra, usuarioCadastrado.Regra);
        //    Assert.Equal(novoUsuario.Nome, usuarioCadastrado.Nome);
        //    Assert.True(usuarioCadastrado.Ativo);
        //}
    }
}
