using System;
using System.Collections.Generic;
using Xunit;
using Agilis.Core.Domain.Extensions;
using Agilis.Test.Mock.Domain.Models.ValueObjects;
using System.Linq;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class LinqExtensionsTests
    {
        [Fact]
        public void MaxOrDefault_VariosItens_Maior()
        {
            //Arrange
            var animais = new List<Animal>
            {
                new Animal{Idade = 10},
                new Animal{Idade = 20},
                new Animal{Idade = 30},
                new Animal{Idade = 1},
                new Animal{Idade = 2},
                new Animal{Idade = 3},
            };

            //Act
            var maior = animais.MaxOrDefault(a => a.Idade, 0);

            //Assert
            Assert.Equal(30, maior);
        }

        [Fact]
        public void MaxOrDefault_SemItens_Default()
        {
            //Arrange
            var animais = new List<Animal>();

            //Act
            var maior = animais.MaxOrDefault(a => a.Idade, -1);

            //Assert
            Assert.Equal(-1, maior);
        }

        [Fact]
        public void MaxOrDefault_SemItensSemDefault_Zero()
        {
            //Arrange
            var animais = new List<Animal>();

            //Act
            var maior = animais.MaxOrDefault(a => a.Idade);

            //Assert
            Assert.Equal(0, maior);
        }

        [Fact]
        public void Move_Desordenado_Ordenado()
        {
            //Arrange
            var listaOriginal = new List<int> { 1, 3, 2, 4, 5, 6 };
            var listaOrdenada = new List<int> { 1, 2, 3, 4, 5, 6 };

            //Act
            var resultado = listaOriginal.Move(1, 2);

            //Assert
            Assert.True(resultado.SequenceEqual(listaOrdenada));
        }

        [Fact]
        public void ObterAutoNumeracao_SemDadosSemRestricoes_Um()
        {
            //Arrange
            var animais = new List<Animal>();

            //Act
            var proximo = animais.ObterAutoNumeracao(a => a.Idade);

            //Assert
            Assert.Equal(1, proximo);
        }

        [Fact]
        public void ObterAutoNumeracao_ComDadosSemRestricoes_Maior()
        {
            //Arrange
            var animais = new List<Animal>
            {
                new Animal{Idade = 10},
                new Animal{Idade = 20},
                new Animal{Idade = 30},
                new Animal{Idade = 1},
                new Animal{Idade = 2},
                new Animal{Idade = 3},
            };

            //Act
            var proximo = animais.ObterAutoNumeracao(a => a.Idade);

            //Assert
            Assert.Equal(31, proximo);
        }

        [Fact]
        public void ObterAutoNumeracao_ComDadosMinimo50_50()
        {
            //Arrange
            var animais = new List<Animal>
            {
                new Animal{Idade = 10},
                new Animal{Idade = 20},
                new Animal{Idade = 30},
                new Animal{Idade = 1},
                new Animal{Idade = 2},
                new Animal{Idade = 3},
            };

            //Act
            var proximo = animais.ObterAutoNumeracao(a => a.Idade, numeroMinimo: 50);

            //Assert
            Assert.Equal(50, proximo);
        }

        [Fact]
        public void ObterAutoNumeracao_ComDadosMaximo20_1()
        {
            //Arrange
            var animais = new List<Animal>
            {
                new Animal{Idade = 10},
                new Animal{Idade = 20},
                new Animal{Idade = 30},
                new Animal{Idade = 1},
                new Animal{Idade = 2},
                new Animal{Idade = 3},
            };

            //Act
            var proximo = animais.ObterAutoNumeracao(a => a.Idade, numeroMaximo: 20);

            //Assert
            Assert.Equal(1, proximo);
        }

        [Fact]
        public void Split_6DadosValidos2PorPagina_3Paginas2RegistrosCada()
        {
            //Arrange
            var animais = new List<Animal>
            {
                new Animal{Idade = 10},
                new Animal{Idade = 20},
                new Animal{Idade = 30},
                new Animal{Idade = 1},
                new Animal{Idade = 2},
                new Animal{Idade = 3},
            };

            //Act
            var paginas = animais.Split(2);

            //Assert
            Assert.Equal(3, paginas.Count());
            Assert.Equal(2, paginas.ElementAt(0).Count());
            Assert.Equal(2, paginas.ElementAt(1).Count());
            Assert.Equal(2, paginas.ElementAt(2).Count());
        }

    }

}
