using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Trabalho;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class ProdutoTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var produto = ProdutoMock.ObterValido();

            //Assert
            Assert.True(produto.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var produto = new Produto(UsuarioMock.ObterValido(),
                                      nome);

            //Assert
            Assert.True(produto.Invalid);
        }
        
        [Fact]
        public void Construtor_UsuarioNulo_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(null, "Produto 1");

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void Construtor_UsuarioInvalid_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(UsuarioMock.ObterInvalido(), "Produto 1");

            //Assert
            Assert.True(produto.Invalid);
        }
    }
}
