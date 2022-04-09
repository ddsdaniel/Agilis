using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ObterApenasNumeros(this String source)
        {
            string apenasNumeros = new String(source.Where(Char.IsDigit).ToArray());
            return apenasNumeros;
        }

        public static string Left(this String texto, int tamanho)
        {
            if (texto.Length < tamanho)
                return texto;

            return texto.Substring(0, tamanho);
        }

        public static string Right(this string texto, int tamanho)
        {
            return texto.Substring(texto.Length - tamanho, tamanho);
        }

        public static string ObterPrimeiraPalavra(this string texto)
        {
            if (String.IsNullOrEmpty(texto))
                return texto;

            const string PATTERN = @"^([\w\-]+)";
            var result = Regex.Match(texto, PATTERN);
            return result.Value;
        }

        public static string ObterApenasLetras(this String texto)
        {
            var apenasLetras = new String(texto.Where(Char.IsLetter).ToArray());
            return apenasLetras;
        }

        public static bool IsDate(this string tempDate)
        {
            var formats = new[] { "dd/MM/yyyy" };
            return DateTime.TryParseExact(tempDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static bool IsNumeric(this string text) => double.TryParse(text, out _);

        /// <summary>
        /// Converte uma string base64 no conteúdo original do arquivo
        /// </summary>
        /// <param name="base64">Input em formato base64</param>
        /// <returns>Conteúdo original do arquivo</returns>
        public static string FromBase64(this string base64)
        {
            var bytes = Convert.FromBase64String(base64.Split(',')[1]);
            var restultado = Encoding.Default.GetString(bytes);
            return restultado;
        }
    }
}
