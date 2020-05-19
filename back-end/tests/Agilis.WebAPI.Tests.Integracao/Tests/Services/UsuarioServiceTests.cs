using Agilis.Domain.Abstractions.Services.Pessoas;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.WebAPI.Tests.Integracao.Abstractions;
using Agilis.WebAPI.Tests.Integracao.Extensions;
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
    }
}
