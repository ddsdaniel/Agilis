using Agilis.Core.Domain.Extensions;
using Xunit;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("123asdas-sdfsadf.asdfasd,asdfasd3213asdf54", "123321354")]
        [InlineData("palavra", "")]
        [InlineData("1,98", "198")]
        [InlineData("1.97", "197")]
        [InlineData("123", "123")]
        [InlineData("-123", "123")]
        [InlineData("286.802.470-06", "28680247006")]
        [InlineData("04.369.367/0001-38", "04369367000138")]
        public void ObterApenasNumeros_CaracteresDiversos_Numeros(string input, string esperado)
        {
            //Arrange & Act
            var atual = input.ObterApenasNumeros();

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("Gerador de CPF", 4, "Gera")]
        [InlineData("Daniel Dorneles da Silva", 6, "Daniel")]
        [InlineData("Validador de CNPJ", 6, "Valida")]
        public void Left_PalavraInteira_CaracteresEsquerda(string input, int caracteres, string esperado)
        {
            //Arrange & Act
            var atual = input.Left(caracteres);

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("Gerador de CPF", 4, " CPF")]
        [InlineData("Daniel Dorneles da Silva", 6, " Silva")]
        [InlineData("Validador de CNPJ", 6, "e CNPJ")]
        public void Right_PalavraInteira_CaracteresDireita(string input, int caracteres, string esperado)
        {
            //Arrange & Act
            var atual = input.Right(caracteres);

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("Gerador de CPF", "Gerador")]
        [InlineData("Daniel Dorneles da Silva", "Daniel")]
        [InlineData("Validador de CNPJ", "Validador")]
        [InlineData("Bola", "Bola")]
        public void ObterPrimeiraPalavra_Frase_PrimeiraPalavra(string input, string esperado)
        {
            //Arrange & Act
            var atual = input.ObterPrimeiraPalavra();

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("123asdas-sdfsadf.asdfasd,asdfasd3213asdF54", "asdassdfsadfasdfasdasdfasdasdF")]
        [InlineData("palavra", "palavra")]
        [InlineData("1,98", "")]
        [InlineData("1.97", "")]
        [InlineData("123", "")]
        [InlineData("-123", "")]
        [InlineData("286.802.470-06", "")]
        [InlineData("04.369.367/0001-38", "")]
        public void ObterApenasLetras_CaracteresDiversos_Letras(string input, string esperado)
        {
            //Arrange & Act
            var atual = input.ObterApenasLetras();

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("15/12/2015", true)]
        [InlineData("30/02/2021", false)]
        [InlineData("01/13/2021", false)]
        [InlineData("", false)]
        [InlineData("string", false)]
        [InlineData("123", false)]
        [InlineData("true", false)]
        public void IsDate_ConformeParametro_ConformeParametro(string input, bool esperado)
        {
            //Arrange & Act
            var atual = input.IsDate();

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("123asdas-sdfsadf.asdfasd,asdfasd3213asdf54", false)]
        [InlineData("string", false)]
        [InlineData("1,98", true)]
        [InlineData("1,9,8", false)]
        [InlineData("1 8", false)]
        [InlineData("1.97", true)]
        [InlineData("123", true)]
        [InlineData("-123", true)]
        [InlineData("--123", false)]
        [InlineData("1-23", false)]
        [InlineData("286.802.470-06", false)]
        [InlineData("04.369.367/0001-38", false)]
        public void IsNumeric_ConformeParametro_ConformeParametro(string input, bool esperado)
        {
            //Arrange & Act
            var atual = input.IsNumeric();

            //Assert
            Assert.Equal(esperado, atual);
        }

        [Theory]
        [InlineData("base64,bGluaGExCmxpbmhhMg==", "linha1\nlinha2")]
        public void FromBase64_Base64_ConteudoArquivo(string input, string esperado)
        {
            //Arrange & Act
            var atual = input.FromBase64();

            //Assert
            Assert.Equal(esperado, atual);
        }

    }

}
