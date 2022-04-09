using System;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Extensions;
using Xunit;

namespace Agilis.Test.Unit.Domain.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void GetValueOrMin_DataValida_DataValida()
        {
            //Arrange
            DateTime? dataOriginal = DateTime.Now;

            //Act
            var resultado = dataOriginal.GetValueOrMin();

            //Assert
            Assert.Equal(dataOriginal.Value, resultado);
        }

        [Fact]
        public void GetValueOrMin_DataNull_Min()
        {
            //Arrange
            DateTime? dataOriginal = null;

            //Act
            var resultado = dataOriginal.GetValueOrMin();

            //Assert
            Assert.Equal(DateTime.MinValue, resultado);
        }

        [Fact]
        public void GetValueOrMax_DataValida_DataValida()
        {
            //Arrange
            DateTime? dataOriginal = DateTime.Now;

            //Act
            var resultado = dataOriginal.GetValueOrMax();

            //Assert
            Assert.Equal(dataOriginal.Value, resultado);
        }

        [Fact]
        public void GetValueOrMax_DataNull_Max()
        {
            //Arrange
            DateTime? dataOriginal = null;

            //Act
            var resultado = dataOriginal.GetValueOrMax();

            //Assert
            Assert.Equal(DateTime.MaxValue, resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(25)]
        public void ObterPrimeiraDataMes_DiaParametro_Dia1(int dia)
        {
            //Arrange
            var dataOriginal = new DateTime(DateTime.Today.Year, DateTime.Today.Month, dia);

            //Act
            var resultado = dataOriginal.ObterPrimeiraDataMes();

            //Assert
            Assert.Equal(1, resultado.Day);
            Assert.Equal(dataOriginal.Month, resultado.Month);
            Assert.Equal(dataOriginal.Year, resultado.Year);
            Assert.Equal(0, resultado.Hour);
            Assert.Equal(0, resultado.Minute);
            Assert.Equal(0, resultado.Second);
            Assert.Equal(0, resultado.Millisecond);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(25)]
        public void ObterUltimoDataMes_DadosValidos_Valid(int dia)
        {
            //Arrange
            var dataOriginal = new DateTime(DateTime.Today.Year, 12, dia);

            //Act
            var resultado = dataOriginal.ObterUltimoDataMes();

            //Assert
            Assert.Equal(31, resultado.Day);
            Assert.Equal(dataOriginal.Month, resultado.Month);
            Assert.Equal(dataOriginal.Year, resultado.Year);
            Assert.Equal(0, resultado.Hour);
            Assert.Equal(0, resultado.Minute);
            Assert.Equal(0, resultado.Second);
            Assert.Equal(0, resultado.Millisecond);
        }

        [Fact]
        public void RemoverMinutos_DadosValidos_Valid()
        {
            //Arrange
            var dataHoraOriginal = DateTime.Now;

            //Act
            var resultado = dataHoraOriginal.RemoverMinutos();

            //Assert
            Assert.Equal(dataHoraOriginal.Day, resultado.Day);
            Assert.Equal(dataHoraOriginal.Month, resultado.Month);
            Assert.Equal(dataHoraOriginal.Year, resultado.Year);
            Assert.Equal(dataHoraOriginal.Hour, resultado.Hour);
            Assert.Equal(0, resultado.Minute);
            Assert.Equal(dataHoraOriginal.Second, resultado.Second);
            Assert.Equal(dataHoraOriginal.Millisecond, resultado.Millisecond);
        }

        [Fact]
        public void RemoverSegundos_DadosValidos_Valid()
        {
            //Arrange
            var dataHoraOriginal = DateTime.Now;

            //Act
            var resultado = dataHoraOriginal.RemoverSegundos();

            //Assert
            Assert.Equal(dataHoraOriginal.Day, resultado.Day);
            Assert.Equal(dataHoraOriginal.Month, resultado.Month);
            Assert.Equal(dataHoraOriginal.Year, resultado.Year);
            Assert.Equal(dataHoraOriginal.Hour, resultado.Hour);
            Assert.Equal(dataHoraOriginal.Minute, resultado.Minute);
            Assert.Equal(0, resultado.Second);
            Assert.Equal(dataHoraOriginal.Millisecond, resultado.Millisecond);
        }

        [Fact]
        public void RemoverMiliSegundos_DadosValidos_Valid()
        {
            //Arrange
            var dataHoraOriginal = DateTime.Now;

            //Act
            var resultado = dataHoraOriginal.RemoverMiliSegundos();

            //Assert
            Assert.Equal(dataHoraOriginal.Day, resultado.Day);
            Assert.Equal(dataHoraOriginal.Month, resultado.Month);
            Assert.Equal(dataHoraOriginal.Year, resultado.Year);
            Assert.Equal(dataHoraOriginal.Hour, resultado.Hour);
            Assert.Equal(dataHoraOriginal.Minute, resultado.Minute);
            Assert.Equal(dataHoraOriginal.Second, resultado.Second);
            Assert.Equal(0, resultado.Millisecond);
        }

        [Fact]
        public void AddWeeks_DadosValidos_Valid()
        {
            //Arrange
            var dataHoraOriginal = DateTime.Now;

            //Act
            var resultado = dataHoraOriginal.AddWeeks(3);

            //Assert
            Assert.Equal(dataHoraOriginal.AddDays(21), resultado);
        }

        [Theory]
        [InlineData(1, "Janeiro")]
        [InlineData(2, "Fevereiro")]
        [InlineData(3, "Março")]
        [InlineData(4, "Abril")]
        [InlineData(5, "Maio")]
        [InlineData(6, "Junho")]
        [InlineData(7, "Julho")]
        [InlineData(8, "Agosto")]
        [InlineData(9, "Setembro")]
        [InlineData(10, "Outubro")]
        [InlineData(11, "Novembro")]
        [InlineData(12, "Dezembro")]
        public void ObterNomeMes_DadosValidos_Valid(int mes, string nome)
        {
            //Arrange
            var dataOriginal = new DateTime(DateTime.Today.Year, mes, 1);

            //Act
            var resultado = dataOriginal.ObterNomeMes();

            //Assert
            Assert.Equal(nome, resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void WeekDiff_ConformeParametros_DiferencaCorreta(int semanas)
        {
            //Arrange
            var dataOriginal = DateTime.Now.AddDays(7 * semanas);

            //Act
            var resultado = DateTime.Now.WeekDiff(dataOriginal);

            //Assert
            Assert.Equal(semanas, resultado);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void MonthDiff_ConformeParametros_DiferencaCorreta(int meses)
        {
            //Arrange
            var dataOriginal = DateTime.Now.AddMonths(meses);

            //Act
            var resultado = DateTime.Now.MonthDiff(dataOriginal);

            //Assert
            Assert.Equal(meses, resultado);
        }

        [Theory]
        [InlineData(10, 2015, "OUT/15")]
        [InlineData(1, 2020, "JAN/20")]
        [InlineData(8, 2050, "AGO/50")]
        [InlineData(9, 1999, "SET/99")]
        public void ObterMesAno_ConformeParametros_Valid(int mes, int ano, string esperado)
        {
            //Arrange
            var dataOriginal = new DateTime(ano, mes, 1);

            //Act
            var resultado = dataOriginal.ObterMesAno();

            //Assert
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(1, 10, 2015, TipoPeriodo.Diario, 1, 10, 2015)]
        [InlineData(15, 1, 2020, TipoPeriodo.Semanal, 12, 1, 2020)]
        [InlineData(20, 8, 2050, TipoPeriodo.Quinzenal, 16, 8, 2050)]
        [InlineData(25, 9, 1999, TipoPeriodo.Mensal, 1, 9, 1999)]
        public void Arredondar_ConformeParametros_DataArredondada(int dia1, int mes1, int ano1, TipoPeriodo tipoPeriodo, int dia2, int mes2, int ano2)
        {
            //Arrange
            var dataOriginal = new DateTime(ano1, mes1, dia1);
            var esperado = new DateTime(ano2, mes2, dia2);

            //Act
            var resultado = dataOriginal.Arredondar(tipoPeriodo);

            //Assert
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData(1, 10, 2015, TipoPeriodo.Diario, "01/OUT")]
        [InlineData(10, 1, 2020, TipoPeriodo.Semanal, "10/JAN")]
        [InlineData(25, 8, 2050, TipoPeriodo.Quinzenal, "25/AGO")]
        [InlineData(5, 9, 1999, TipoPeriodo.Mensal, "SET/99")]
        public void ObterNome_ConformeParametros_NomeCorreto(int dia, int mes, int ano, TipoPeriodo tipoPeriodo, string esperado)
        {
            //Arrange
            var dataOriginal = new DateTime(ano, mes, dia);

            //Act
            var resultado = dataOriginal.ObterNome(tipoPeriodo);

            //Assert
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void ObterDataFinal_DadosValidos_Valid()
        {
            //Arrange
            var dataOriginal = new DateTime(2021, 12, 24);
            var dataEsperada = new DateTime(2022, 1, 9);

            //Act
            var resultado = dataOriginal.ObterDataFinal(TipoPeriodo.Semanal, 3);

            //Assert
            Assert.Equal(dataEsperada, resultado);
        }

        [Fact]
        public void ObterProxima_DadosValidos_Valid()
        {
            //Arrange
            var dataOriginal = new DateTime(2021, 12, 24);
            var dataEsperada = new DateTime(2021, 12, 31);

            //Act
            var resultado = dataOriginal.ObterProxima(TipoPeriodo.Semanal);

            //Assert
            Assert.Equal(dataEsperada, resultado);
        }

    }

}
