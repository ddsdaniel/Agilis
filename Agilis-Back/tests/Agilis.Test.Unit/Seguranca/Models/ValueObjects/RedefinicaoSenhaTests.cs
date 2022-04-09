using Agilis.Infra.Seguranca.Models.ValueObjects;
using Agilis.Test.Mock.Seguranca.Models.ValueObjects;
using Xunit;

namespace Agilis.Test.Unit.Seguranca.Models.ValueObjects
{
    public class RedefinicaoSenhaTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var redefinicaoSenha = RedefinicaoSenhaMocks.ObterValido();

            //Assert
            Assert.True(redefinicaoSenha.Valid);
        }

        [Fact]
        public void Construtor_NovaSenhaNull_Invalid()
        {
            //Arrange & Act
            var redefinicaoSenha = RedefinicaoSenhaMocks.ObterComNovaSenha(null);

            //Assert
            Assert.True(redefinicaoSenha.Invalid);
        }

        [Fact]
        public void Construtor_SenhasDiferentes_Invalid()
        {
            //Arrange
            var novaSenha = SenhaMocks.ObterComConteudo("1234");
            var confirmaSenha = SenhaMocks.ObterComConteudo("4321");

            //Act
            var redefinicaoSenha = new RedefinicaoSenha(confirmaSenha, novaSenha);

            //Assert
            Assert.True(redefinicaoSenha.Invalid);
            Assert.True(confirmaSenha.Valid);
            Assert.True(novaSenha.Valid);
            Assert.NotEqual(novaSenha.Conteudo, confirmaSenha.Conteudo);
        }

        [Fact]
        public void Construtor_NovaSenhaInvalid_Invalid()
        {
            //Arrange
            var senhaInvalida = SenhaMocks.ObterComConteudo(null);

            //Act
            var redefinicaoSenha = RedefinicaoSenhaMocks.ObterComNovaSenha(senhaInvalida);

            //Assert
            Assert.True(redefinicaoSenha.Invalid);
            Assert.True(senhaInvalida.Invalid);
        }

        [Fact]
        public void Construtor_ConfirmaSenhaNull_Invalid()
        {
            //Arrange & Act
            var redefinicaoSenha = RedefinicaoSenhaMocks.ObterComConfirmaSenha(null);

            //Assert
            Assert.True(redefinicaoSenha.Invalid);
        }

        [Fact]
        public void Construtor_ConfirmaSenhaInvalid_Invalid()
        {
            //Arrange
            var senhaInvalida = SenhaMocks.ObterComConteudo(null);

            //Act
            var redefinicaoSenha = RedefinicaoSenhaMocks.ObterComConfirmaSenha(senhaInvalida);

            //Assert
            Assert.True(redefinicaoSenha.Invalid);
            Assert.True(senhaInvalida.Invalid);
        }

    }

}
