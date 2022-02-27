using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Enums;
using System.Threading.Tasks;
using Xunit;
using Agilis.Application.Services.Seguranca;
using Agilis.Test.Integration.Application.Constantes;
using Agilis.Infra.Seguranca.Models.Entities;
using Moq;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Test.Integration.Config;
using Agilis.Test.Integration.Extensions;
using System.Linq;

namespace Agilis.Test.Integration.Application.Services
{
    [Collection(nameof(IntegrationWebTestFixtureCollection))]
    public class UsuarioCrudAppServiceTests
    {
        private readonly IntegrationTestFixture _integrationTestFixture;

        public UsuarioCrudAppServiceTests(IntegrationTestFixture integrationTestFixture)
        {
            _integrationTestFixture = integrationTestFixture;
        }

        [Fact]
        public async Task AdicionarAsync_DadosValidos_Valid()
        {
            //Arrange
            var novoUsuarioViewModel = new UsuarioCadastroViewModel
            {
                Nome = UsuarioConts.Nome,
                Sobrenome = UsuarioConts.Sobrenome,
                Email = UsuarioConts.Email,
                Regra = RegraUsuario.Usuario,
                Senha = UsuarioConts.Senha,
                ConfirmaSenha = UsuarioConts.Senha
            };

            var usuarioRepository = _integrationTestFixture.AutoMocker.MockarRepositoryCatalogo<Usuario>();

            var usuarioCrudAppService = _integrationTestFixture.AutoMocker.CreateInstance<UsuarioCrudAppService>();

            //Act
            await usuarioCrudAppService.AdicionarAsync(novoUsuarioViewModel);

            //Assert
            Assert.True(usuarioCrudAppService.Valid);
            var novoUsuarioDomain = _integrationTestFixture.Mapper.Map<Usuario>(novoUsuarioViewModel);
            usuarioRepository.Verify(r => r.AdicionarAsync(novoUsuarioDomain), Times.Once);
            _integrationTestFixture.AutoMocker.GetMock<IUnitOfWorkCatalogo>().Verify(uow => uow.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task AdicionarAsync_DadosInvalidos_Invalid()
        {
            //Arrange
            var usuarioInvalidoViewModel = new UsuarioCadastroViewModel
            {
                Nome = null,
                Sobrenome = "",
                Email = "invalido",
                Regra = RegraUsuario.Usuario,
                Senha = UsuarioConts.Senha,
                ConfirmaSenha = UsuarioConts.Senha.Reverse().ToString()
            };

            var usuarioRepository = _integrationTestFixture.AutoMocker.MockarRepositoryCatalogo<Usuario>();

            var usuarioCrudAppService = _integrationTestFixture.AutoMocker.CreateInstance<UsuarioCrudAppService>();

            //Act
            await usuarioCrudAppService.AdicionarAsync(usuarioInvalidoViewModel);

            //Assert
            var novoUsuarioInvalidoDomain = _integrationTestFixture.Mapper.Map<Usuario>(usuarioInvalidoViewModel);
            Assert.True(usuarioCrudAppService.Invalid);
            Assert.True(novoUsuarioInvalidoDomain.Invalid);
            usuarioRepository.Verify(r => r.AdicionarAsync(novoUsuarioInvalidoDomain), Times.Never);
            _integrationTestFixture.AutoMocker.GetMock<IUnitOfWorkCatalogo>().Verify(uow => uow.CommitAsync(), Times.Never);
        }

        //[Fact]
        //public void AlterarAsync_DadosValidos_Valid()
        //{
        //    //Arrange
        //    var usuarioCrudAppService = UsuarioCrudAppServiceMocks.ObterValido();

        //    //Act
        //    usuarioCrudAppService.AlterarAsync();

        //    //Assert
        //    Assert.True(usuarioCrudAppService.Valid);
        //}

        //[Fact]
        //public void ConsultarTodos_DadosValidos_Valid()
        //{
        //    //Arrange
        //    var usuarioCrudAppService = UsuarioCrudAppServiceMocks.ObterValido();

        //    //Act
        //    usuarioCrudAppService.ConsultarTodos();

        //    //Assert
        //    Assert.True(usuarioCrudAppService.Valid);
        //}

    }

}
