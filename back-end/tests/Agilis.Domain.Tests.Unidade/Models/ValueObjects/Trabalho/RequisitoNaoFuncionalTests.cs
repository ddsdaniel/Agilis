using Agilis.Domain.Enums;
using Agilis.Domain.Mocks.Entities.Pessoas;
using Agilis.Domain.Mocks.ValueObjects.Trabalho;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Xunit;

namespace Agilis.Domain.Tests.Unidade.Models.ValueObjects.Trabalho
{
    public class RequisitoNaoFuncionalTests
    {
        [Fact]
        public void Construtor_DadosValidos_Valid()
        {
            //Arrange & Act
            var rnf = RequisitoNaoFuncionalMock.ObterValido();

            //Assert
            Assert.True(rnf.Valid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Construtor_NumeroInvalido_Invalid(int numero, bool resultadoEsperado)
        {
            //Arrange & Act
            var rnf = new RequisitoNaoFuncional(numero,
                                                nameof(RequisitoNaoFuncional),
                                                TipoRequisitoNaoFuncional.Automacao,
                                                UsuarioMock.ObterValido()
                                                );

            //Assert
            Assert.Equal(resultadoEsperado, rnf.Valid);
        }

        [Theory]
        [InlineData("Descrição válida", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Construtor_DescricaoInvalido_Invalid(string descricao, bool resultadoEsperado)
        {
            //Arrange & Act
            var rnf = new RequisitoNaoFuncional(1,
                                                descricao,
                                                TipoRequisitoNaoFuncional.Automacao,
                                                UsuarioMock.ObterValido()
                                                );

            //Assert
            Assert.Equal(resultadoEsperado, rnf.Valid);
        }

        [Fact]
        public void Construtor_AutorNulo_Invalid()
        {
            //Arrange & Act
            var rnf = new RequisitoNaoFuncional(1,
                                                nameof(RequisitoNaoFuncional),
                                                TipoRequisitoNaoFuncional.Automacao,
                                                null
                                                );

            //Assert
            Assert.True(rnf.Invalid);
        }

        [Fact]
        public void Construtor_AutorInvalido_Invalid()
        {
            //Arrange & Act
            var rnf = new RequisitoNaoFuncional(1,
                                                nameof(RequisitoNaoFuncional),
                                                TipoRequisitoNaoFuncional.Automacao,
                                                UsuarioMock.ObterInvalido()
                                                );

            //Assert
            Assert.True(rnf.Invalid);
        }        
    }
}
