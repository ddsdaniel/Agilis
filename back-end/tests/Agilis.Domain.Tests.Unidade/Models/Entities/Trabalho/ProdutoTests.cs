using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Mocks.Entities.Trabalho;
using System;
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
            var produto = new Produto(nome);

            //Assert
            Assert.True(produto.Invalid);
        }

    }
}
