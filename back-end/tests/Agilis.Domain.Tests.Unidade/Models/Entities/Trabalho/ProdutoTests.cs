﻿using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Pessoas;
using Agilis.Domain.Tests.Unidade.Mocks.Entities.Trabalho;
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
            var produto = new Produto(Guid.NewGuid(), nome);

            //Assert
            Assert.True(produto.Invalid);
        }
        
        [Fact]
        public void Construtor_UsuarioIdEmpty_Invalid()
        {
            //Arrange & Act
            var produto = new Produto(Guid.Empty, "Produto 1");

            //Assert
            Assert.True(produto.Invalid);
        }

    }
}
