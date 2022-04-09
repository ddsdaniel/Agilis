using Agilis.Core.Domain.Models.Entities;
using Agilis.Test.Mock.Domain.Models.Entities;
using Agilis.Test.Mock.Domain.Models.ValueObjects;
using System.Collections.Generic;
using Xunit;

namespace Agilis.Test.Unit.Domain.Models.ValueObjects
{
    public class NotificacaoTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var notificacao = NotificacaoMocks.ObterValido();

            //Assert
            Assert.True(notificacao.Valid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_TituloInvalido_Invalid(string titulo)
        {
            //Arrange & Act
            var notificacao = NotificacaoMocks.ObterComTitulo(titulo);

            //Assert
            Assert.True(notificacao.Invalid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Construtor_CorpoInvalido_Invalid(string corpo)
        {
            //Arrange & Act
            var notificacao = NotificacaoMocks.ObterComCorpo(corpo);

            //Assert
            Assert.True(notificacao.Invalid);
        }

        [Fact]
        public void Construtor_DispositivosInvalido_Invalid()
        {
            //Arrange
            var dispositivo = DispositivoMocks.ObterComToken(null);

            //Act
            var notificacao = NotificacaoMocks.ObterComDispositivos(new List<Dispositivo> { dispositivo });

            //Assert
            Assert.True(dispositivo.Invalid);
            Assert.True(notificacao.Invalid);
        }

        [Fact]
        public void Construtor_DispositivosNull_Invalid()
        {
            //Arrange & Act
            var notificacao = NotificacaoMocks.ObterComDispositivos(null);

            //Assert
            Assert.True(notificacao.Invalid);
        }

    }

}
