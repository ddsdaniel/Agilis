using Agilis.Core.Domain.Extensions;
using Agilis.Test.Mock.Domain.Enums;
using Xunit;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(TesteEnum.Janeiro, "Janeiro")]
        [InlineData(TesteEnum.Marco, "Março")]
        [InlineData(TesteEnum.QuartaFeira, "Quarta-feira")]
        [InlineData(TesteEnum.Laranja, "Laranja")]
        [InlineData(TesteEnum.Reflexao, "Reflexão")]
        public void GetDescription_Enum_DescricaoDoEnum(TesteEnum testeEnum, string esperado)
        {
            //Arrange & Act
            var atual = testeEnum.GetDescription();

            //Assert
            Assert.Equal(esperado, atual);
        }
    }

}
