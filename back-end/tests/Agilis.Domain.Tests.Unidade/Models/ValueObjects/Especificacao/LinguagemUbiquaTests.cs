using Agilis.Domain.Mocks.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.ValueObjects.Especificacao
{
    public class LinguagemUbiquaTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var linguagemUbiqua = LinguagemUbiquaMock.ObterValida();

            //Assert
            Assert.True(linguagemUbiqua.Valid);
        }

        [Fact]
        public void Construtor_JargoesNulo_Invalid()
        {
            //Arrange & Act
            var linguagemUbiqua = new LinguagemUbiqua(null);

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
        }

        [Fact]
        public void Construtor_JargoesInvalido_Invalid()
        {
            //Arrange & Act
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio> { JargaoDoNegocioMock.ObterInvalido() });

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
        }

        [Theory]
        [InlineData("J1", "S1")]
        [InlineData("J2", "S2")]
        [InlineData("J3", "S3")]
        [InlineData("J4", "S4")]
        [InlineData("J5", "S5")]
        [InlineData("J6", "S6")]
        public void Adicionar_JargaoValido_Adicionado(string jargao, string significado)
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Adicionar(jargao, significado);

            //Assert
            Assert.True(linguagemUbiqua.Valid);
            Assert.Equal(contAntes + 1, linguagemUbiqua.Jargoes.Count);

            var ultimo = linguagemUbiqua.Jargoes.Last();
            Assert.Equal(jargao, ultimo.Jargao);
            Assert.Equal(significado, ultimo.Significado);
        }

        [Fact]
        public void Adicionar_Duplicado_NaoAdicionado()
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Adicionar("J1", "S1");
            linguagemUbiqua.Adicionar("J1", "S2");

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
            Assert.Equal(contAntes + 1, linguagemUbiqua.Jargoes.Count);

            var ultimo = linguagemUbiqua.Jargoes.Last();
            Assert.Equal("J1", ultimo.Jargao);
            Assert.Equal("S1", ultimo.Significado);
        }

        [Fact]
        public void Adicionar_JargaoInvalido_NaoAdicionado()
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Adicionar("", "S1");
            linguagemUbiqua.Adicionar(null, "S2");

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
            Assert.Equal(contAntes, linguagemUbiqua.Jargoes.Count);
        }

        [Fact]
        public void Adicionar_SignificadoInvalido_NaoAdicionado()
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Adicionar("J1", "");
            linguagemUbiqua.Adicionar("J2", null);

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
            Assert.Equal(contAntes, linguagemUbiqua.Jargoes.Count);
        }

        [Theory]
        [InlineData("Jargão", "Jargão")]
        [InlineData("Jargão", "jargão")]
        [InlineData("Jargão", "JARGÃO")]
        public void Remover_DadosValidos_Removido(string jargaoParaAdicionar, string jargaoParaRemover)
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            linguagemUbiqua.Adicionar(jargaoParaAdicionar, "S1");
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Remover(jargaoParaRemover);

            //Assert
            Assert.True(linguagemUbiqua.Valid);
            Assert.Equal(contAntes - 1, linguagemUbiqua.Jargoes.Count);
        }

        [Fact]
        public void Remover_JargaoNaoEncontrado_Invalid()
        {
            //Arrange
            var linguagemUbiqua = new LinguagemUbiqua(new List<JargaoDoNegocio>());
            linguagemUbiqua.Adicionar("J1", "S1");
            var contAntes = linguagemUbiqua.Jargoes.Count;

            //Act
            linguagemUbiqua.Remover("J2");

            //Assert
            Assert.True(linguagemUbiqua.Invalid);
            Assert.Equal(contAntes, linguagemUbiqua.Jargoes.Count);
        }
       
    }
}
