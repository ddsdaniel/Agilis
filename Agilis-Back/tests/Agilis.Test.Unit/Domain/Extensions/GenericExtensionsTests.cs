using Agilis.Core.Domain.Extensions;
using Agilis.Test.Mock.Domain.Models.ValueObjects;
using System;
using Xunit;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class GenericExtensionsTests
    {
        [Fact]
        public void Clonar_DadosValidos_ObjetoClonado()
        {
            //Arrange
            var florzinha = new Animal
            {
                Nome = "Florzinha",
                Raca = "Poodle",
                Cor = "Branco",
                DataNascimento = new DateTime(2005, 12, 10),
                Idade = 15,
                Femea = true
            };

            //Act
            var clone = florzinha.Clonar();

            //Assert
            Assert.False(ReferenceEquals(florzinha, clone));
            Assert.Equal(florzinha.Nome, clone.Nome);
            Assert.Equal(florzinha.Raca, clone.Raca);
            Assert.Equal(florzinha.Cor, clone.Cor);
            Assert.Equal(florzinha.DataNascimento, clone.DataNascimento);
            Assert.Equal(florzinha.Idade, clone.Idade);
            Assert.Equal(florzinha.Femea, clone.Femea);
        }

    }

}
