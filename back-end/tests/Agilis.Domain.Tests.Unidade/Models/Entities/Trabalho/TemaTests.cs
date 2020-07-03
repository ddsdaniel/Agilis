﻿using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Mocks.Entities.Trabalho;
using Xunit;
using System;

namespace Agilis.Domain.Tests.Unidade.Models.Entities.Trabalho
{
    public class TemaTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var tema = TemaMock.ObterValido();

            //Assert
            Assert.True(tema.Valid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Construtor_NomeInvalido_Invalid(string nome)
        {
            //Arrange & Act
            var tema = new Tema(nome, Guid.NewGuid());

            //Assert
            Assert.True(tema.Invalid);
        }
    }
}
