using Agilis.WebAPI.Tests.Integracao.Extensions;
using Agilis.WebAPI.ViewModels.Seguranca;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Agilis.WebAPI.Tests.Integracao.Abstractions
{
    public abstract class ControllerTests : Teste
    {
        protected readonly HttpClient _client;        

        public ControllerTests(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
            : base(customWebApplicationFactory)
        {            
            _client = _customWebApplicationFactory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }

        protected async Task<UsuarioLogadoViewModel> Autenticar(LoginViewModel login)
        {
            var response = await _client.PostAsync("api/Usuarios/login", login);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var usuarioLogado = await response.Content.ReadAsAsync<UsuarioLogadoViewModel>();

            Assert.NotNull(usuarioLogado);

            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");

            _client.DefaultRequestHeaders.Add("Authorization", $"{usuarioLogado.TipoToken} {usuarioLogado.Token}");

            return usuarioLogado;
        }
    }
}
