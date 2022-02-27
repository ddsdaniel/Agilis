using Flunt.Validations;
using Agilis.Core.Domain.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class ContractExtensionsTests
    {
        [Fact]
        public void IsValidArray_ArrayComCriticas_Invalid()
        {
            //Arrange
            var contratoOriginal = new Contract();
            var contratoComCriticas = new Contract();
            contratoComCriticas.AddNotification("Propriedade", "Crítica");

            //Act
            contratoOriginal.IsValidArray(new List<Contract> { contratoComCriticas }, "Teste");

            //Assert
            Assert.True(contratoComCriticas.Invalid);
            Assert.True(contratoOriginal.Invalid);
        }

        [Fact]
        public void IsValid_ContratoComCriticas_Invalid()
        {
            //Arrange
            var contratoOriginal = new Contract();
            var contratoComCriticas = new Contract();
            contratoComCriticas.AddNotification("Propriedade", "Crítica");

            //Act
            contratoOriginal.IsValid(contratoComCriticas, "Teste");

            //Assert
            Assert.True(contratoComCriticas.Invalid);
            Assert.True(contratoOriginal.Invalid);
        }

    }

}
