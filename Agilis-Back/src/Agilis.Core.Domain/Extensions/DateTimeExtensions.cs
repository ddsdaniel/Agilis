using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Core.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Retorna a data contida na variável ou <see cref="DateTime.MinValue"/>, caso a variável contenha null
        /// </summary>
        public static DateTime GetValueOrMin(this DateTime? dataHora) => dataHora.GetValueOrDefault(DateTime.MinValue);

        /// <summary>
        /// Retorna a data contida na variável ou <see cref="DateTime.MaxValue"/>, caso a variável contenha null
        /// </summary>
        public static DateTime GetValueOrMax(this DateTime? dataHora) => dataHora.GetValueOrDefault(DateTime.MaxValue);

        public static DateTime ObterPrimeiraDataMes(this DateTime data) => new DateTime(data.Year, data.Month, 1);

        public static DateTime ObterUltimoDataMes(this DateTime data)
        {
            data = data.ObterPrimeiraDataMes();
            data = data.AddMonths(1);
            data = data.AddDays(-1);
            return data;
        }

        public static DateTime RemoverMinutos(this DateTime data)
        {
            data = data.AddMinutes(data.Minute * (-1));
            return data;
        }

        public static DateTime RemoverSegundos(this DateTime data)
        {
            data = data.AddSeconds(data.Second * (-1));
            return data;
        }

        public static DateTime RemoverMiliSegundos(this DateTime data)
        {
            data = data.AddMilliseconds(data.Millisecond * (-1));
            return data;
        }

        public static DateTime AddWeeks(this DateTime data, double weeks)
        {
            return data.AddDays(weeks * 7);
        }

        public static string ObterNomeMes(this DateTime data) => data.Month switch
        {
            1 => "Janeiro",
            2 => "Fevereiro",
            3 => "Março",
            4 => "Abril",
            5 => "Maio",
            6 => "Junho",
            7 => "Julho",
            8 => "Agosto",
            9 => "Setembro",
            10 => "Outubro",
            11 => "Novembro",
            12 => "Dezembro",
            _ => throw new NotImplementedException(),
        };

        public static int WeekDiff(this DateTime source, DateTime outraData)
        {
            var semanas = (int)((outraData - source).TotalDays / 7) + 1;
            return semanas;
        }

        public static int MonthDiff(this DateTime source, DateTime outraData)
        {
            var meses = (int)((outraData - source).TotalDays / 30);// (int)(outraData.Subtract(source).Days / (365.25 / 12));
            return meses;
        }

        public static string ObterMesAno(this DateTime data)
        {
            var nomeMes = data.ObterNomeMes();
            nomeMes = nomeMes.Left(3);
            nomeMes += "/";
            nomeMes += data.Year.ToString().Right(2);
            nomeMes = nomeMes.ToUpper();

            return nomeMes;
        }


        /// <summary>
        /// Arredonda uma data para a primeira ocorrência de um período
        /// </summary>
        /// <param name="source">Data a ser arredondada</param>
        /// <param name="tipoPeriodo">Período em que a data será arredondada</param>
        /// <returns>Data arredondada</returns>
        public static DateTime Arredondar(this DateTime source, TipoPeriodo tipoPeriodo)
        {
            return tipoPeriodo switch
            {
                TipoPeriodo.Diario => source,
                TipoPeriodo.Semanal => source.AddDays(-(int)source.DayOfWeek),
                TipoPeriodo.Quinzenal => source.Day > 15
                    ? new DateTime(source.Year, source.Month, 16)
                    : new DateTime(source.Year, source.Month, 1),
                TipoPeriodo.Mensal => new DateTime(source.Year, source.Month, 1),
                _ => throw new NotImplementedException($"Tipo de período não implementado: {tipoPeriodo}"),
            };
        }

        /// <summary>
        /// Retorna um nome amigável para uma data de acordo com o seu período
        /// </summary>
        /// <param name="source">Data de referência</param>
        /// <param name="tipoPeriodo">Tipo do período do agrupamento em questão</param>
        /// <returns>Nome amigável da data</returns>
        public static string ObterNome(this DateTime source, TipoPeriodo tipoPeriodo)
        {
            switch (tipoPeriodo)
            {
                case TipoPeriodo.Diario:
                case TipoPeriodo.Semanal:
                case TipoPeriodo.Quinzenal:
                    return source.ToString("dd/MMM").ToUpper();
                case TipoPeriodo.Mensal:
                    return ObterMesAno(source);
                default:
                    throw new NotImplementedException($"Tipo de período não implementado: {tipoPeriodo}");
            }
        }

        /// <summary>
        /// Obtém a última ocorrênca de um tipo de período a partir de uma data de referência
        /// </summary>
        /// <param name="source">Data de referência</param>
        /// <param name="tipoPeriodo">Tipo do período do agrupamento em questão</param>
        /// <param name="ocorrencias">Número de ocorrências usado para identificar a última data</param>
        /// <returns>Data final</returns>
        public static DateTime ObterDataFinal(this DateTime source, TipoPeriodo tipoPeriodo, int ocorrencias)
        {
            var dataFinal = Arredondar(source, tipoPeriodo);
            for (int i = 0; i < ocorrencias; i++)
            {
                dataFinal = dataFinal.ObterProxima(tipoPeriodo);
            }
            return dataFinal;
        }

        public static DateTime ObterProxima(this DateTime source, TipoPeriodo tipoPeriodo)
        {
            return tipoPeriodo switch
            {
                TipoPeriodo.Diario => source.AddDays(1),
                TipoPeriodo.Semanal => source.AddDays(7),
                TipoPeriodo.Quinzenal => source.Day > 15
                    ? ObterPrimeiraDataMes(source).AddMonths(1)
                    : new DateTime(source.Year, source.Month, 16),
                TipoPeriodo.Mensal => source.AddMonths(1),
                _ => throw new NotImplementedException($"Tipo de período não implementado: {tipoPeriodo}"),
            };
        }
    }
}
