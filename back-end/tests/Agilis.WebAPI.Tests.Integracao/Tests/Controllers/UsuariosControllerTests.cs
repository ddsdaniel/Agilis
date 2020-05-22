using Agilis.WebAPI.Tests.Integracao.Extensions;
using System.Threading.Tasks;
using Xunit;
using Agilis.WebAPI.ViewModels.Pessoas;
using Agilis.Domain.Enums;
using System.Net;
using Bogus;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.ViewModels.Seguranca;
using Flunt.Notifications;

namespace Agilis.WebAPI.Tests.Integracao.Tests.Controllers
{
    public class UsuariosControllerTests : ControllerTests
    {
        public UsuariosControllerTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
            : base(customWebApplicationFactory)
        {
        }

        [Fact]
        public async Task Post_AdminValido_Ok()
        {
            // Arrange
            var admin = new Faker<UsuarioCadastroViewModel>()
               .CustomInstantiator(p => new UsuarioCadastroViewModel
               {
                   Email = p.Internet.Email("admin"),
                   Nome = "Administrador",
                   Sobrenome = "do Sistema",
                   Regra = RegraUsuario.Admin,
                   Senha = "123456aa",
                   ConfirmaSenha = "123456aa"
               })
               .Generate();

            //Act
            var response = await _client.PostAsync("api/Usuarios", admin);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_UsuarioDuplicado_BadRequest()
        {
            // Arrange
            var duplicado = new Faker<UsuarioCadastroViewModel>()
               .CustomInstantiator(p => new UsuarioCadastroViewModel
               {
                   Email = "duplicado@gmail.com",
                   Nome = "Usuário",
                   Sobrenome = "Duplicado",
                   Regra = RegraUsuario.Usuario,
                   Senha = "123456aa",
                   ConfirmaSenha = "123456aa"
               })
               .Generate();

            //Act
            var response1 = await _client.PostAsync("api/Usuarios", duplicado);
            var response2 = await _client.PostAsync("api/Usuarios", duplicado);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode);
        }

        [Fact]
        public async Task AlterarSenha_DadosCorretos_Ok()
        {
            // Arrange
            var novoUsuario = new Faker<UsuarioCadastroViewModel>()
               .CustomInstantiator(p => new UsuarioCadastroViewModel
               {
                   Email = p.Internet.Email(),
                   Nome = p.Person.FirstName,
                   Sobrenome = p.Person.LastName,
                   Regra = RegraUsuario.Usuario,
                   Senha = "123456aa",
                   ConfirmaSenha = "123456aa"
               })
               .Generate();

            var responsePost = await _client.PostAsync("api/Usuarios", novoUsuario);

            var login = new LoginViewModel
            {
                Email = novoUsuario.Email,
                Senha = novoUsuario.Senha
            };
            var usuarioLogado = await Autenticar(login);


            var alterarSenha = new AlteraSenhaViewModel
            {
                SenhaAtual = novoUsuario.Senha,
                NovaSenha = novoUsuario.Senha + "x",
                ConfirmaSenha = novoUsuario.Senha + "x"
            };


            //Act
            var responsePatch = await _client.PatchAsync($"api/Usuarios/{usuarioLogado.Usuario.Id}", alterarSenha);

            // Assert
            Assert.Equal(HttpStatusCode.OK, responsePost.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responsePatch.StatusCode);

        }

        [Fact]
        public async Task Autenticar_SenhaInvalida_BadRequest()
        {
            //Arrange
            var novoUsuario = new Faker<UsuarioCadastroViewModel>()
               .CustomInstantiator(p => new UsuarioCadastroViewModel
               {
                   Email = p.Internet.Email(),
                   Nome = p.Person.FirstName,
                   Sobrenome = p.Person.LastName,
                   Regra = RegraUsuario.Usuario,
                   Senha = "123456aa",
                   ConfirmaSenha = "123456aa"
               })
               .Generate();

            var responseCadastro = await _client.PostAsync("api/Usuarios", novoUsuario);

            var login = new LoginViewModel { Email = novoUsuario.Email,Senha = "senha-errada-1234"};

            //Act
            var responseLogin = await _client.PostAsync("api/Usuarios/login", login);

            //Assert
            Assert.Equal(HttpStatusCode.OK, responseCadastro.StatusCode); 
            Assert.Equal(HttpStatusCode.BadRequest, responseLogin.StatusCode);

            var notitications = await responseLogin.Content.ReadAsAsync<Notification[]>();
            Assert.NotEmpty(notitications);
        }
    }
}
