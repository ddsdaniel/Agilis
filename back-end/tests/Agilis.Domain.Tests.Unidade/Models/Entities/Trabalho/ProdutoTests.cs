using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Mocks.Entities.Trabalho;
using System;
using Xunit;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System.Collections.Generic;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using System.Linq;

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
            var produto = new Produto(nome, new List<RequisitoNaoFuncional>());

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void Construtor_RnfNulo_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(nameof(Produto), null);

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void Construtor_RnfInvalido_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(nameof(Produto),
                                      new List<RequisitoNaoFuncional> { RequisitoNaoFuncionalMock.ObterInvalido() }
                                      );

            //Assert
            Assert.True(produto.Invalid);
        }

        [Fact]
        public void AdicionarRNF_RnfValido_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;            
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);

            //Act
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Valid);
            Assert.Equal(contRNF + 2, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_RnfNulo_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            produto.AdicionarRNF(null);

            //Assert
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_RnfInvalido_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;
            var rnf = RequisitoNaoFuncionalMock.ObterInvalido();

            //Act
            produto.AdicionarRNF(rnf);

            //Assert
            Assert.True(rnf.Invalid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void AdicionarRNF_NumeroDuplicado_Invalid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var contRNF = produto.RequisitosNaoFuncionais.Count;
            var rnfPrimeiro = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnfDuplicado = RequisitoNaoFuncionalMock.ObterValido(1);

            //Act
            produto.AdicionarRNF(rnfPrimeiro);
            produto.AdicionarRNF(rnfDuplicado);

            //Assert
            Assert.True(rnfPrimeiro.Valid);
            Assert.True(rnfDuplicado.Valid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF + 1, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void RemoverRNF_RnfValido_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            produto.RemoverRNF(1);
            produto.RemoverRNF(2);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Valid);
            Assert.Equal(contRNF - 2, produto.RequisitosNaoFuncionais.Count);
        }

        [Fact]
        public void RemoverRNF_RnNaoEncontrado_Valid()
        {
            //Arrange
            var produto = ProdutoMock.ObterValido();
            var rnf1 = RequisitoNaoFuncionalMock.ObterValido(1);
            var rnf2 = RequisitoNaoFuncionalMock.ObterValido(2);
            produto.AdicionarRNF(rnf1);
            produto.AdicionarRNF(rnf2);
            var contRNF = produto.RequisitosNaoFuncionais.Count;

            //Act
            var rnfNaoEncontrado = produto.RequisitosNaoFuncionais.Max(r => r.Numero) + 1;
            produto.RemoverRNF(rnfNaoEncontrado);

            //Assert
            Assert.True(rnf1.Valid);
            Assert.True(rnf2.Valid);
            Assert.True(produto.Invalid);
            Assert.Equal(contRNF, produto.RequisitosNaoFuncionais.Count);
        }
    }
}
