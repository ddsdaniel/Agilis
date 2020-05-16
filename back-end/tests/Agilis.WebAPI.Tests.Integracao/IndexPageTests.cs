using Agilis.WebAPI.Tests.Integracao.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Agilis.WebAPI.ViewModels.Pessoas;
using Agilis.Domain.Enums;
using System.Net;

namespace Agilis.WebAPI.Tests.Integracao
{
    public class IndexPageTests : IClassFixture<CustomWebApplicationFactory<WebAPI.Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<WebAPI.Startup> _customWebApplicationFactory;

        public IndexPageTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
        {
            _customWebApplicationFactory = customWebApplicationFactory;
            _client = _customWebApplicationFactory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        [Fact]
        public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
        {
            // Arrange
            var admin = new UsuarioCadastroViewModel
            {
                Email = "admin@agilis.com",
                Nome = "Administrador",
                Sobrenome = "do Sistema",
                Regra = RegraUsuario.Admin,
                Senha = "123456aa",
                ConfirmaSenha = "123456aa"
            };
            
            //Act
            var response = await _client.PostAsync("api/Entrar", admin);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
        }

        //HttpResponseMessage response = Client.PostAsync("api/Entrar", usuarioEntrarViewModel).Result;

        //        if (response.StatusCode != HttpStatusCode.OK)
        //            throw new Exception("Usuario nao autorizado");

        //RespostaEntrarViewModel token = response.Content.ReadAsAsync<RespostaEntrarViewModel>().Result;

        //        if (token == null)
        //            throw new Exception("Falha ao obter token");

        //        if (Client.DefaultRequestHeaders.Contains("Authorization"))
        //            Client.DefaultRequestHeaders.Remove("Authorization");

        //        Client.DefaultRequestHeaders.Add("Authorization", $"{token.TipoToken} {token.Token}");
    }
}
