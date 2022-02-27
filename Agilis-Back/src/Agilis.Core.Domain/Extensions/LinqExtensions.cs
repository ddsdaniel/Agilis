using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Core.Domain.Extensions
{
    public static class LinqExtensions
    {
        public static int MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int defaultValue)
            => source.Any() ? source.Max(selector) : defaultValue;

        public static int MaxOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
            => MaxOrDefault(source, selector, 0);

        public static IEnumerable<TSource> Move<TSource>(this IEnumerable<TSource> source, int from, int to)
        {
            var list = source.ToList();
            var item = list[from];
            list.RemoveAt(from);
            list.Insert(to, item);
            return list;
        }

        public static double ObterAutoNumeracao<TSource>(this IEnumerable<TSource> source,
                                                     Func<TSource, double> selector,
                                                     double? numeroMinimo = null,
                                                     double? numeroMaximo = null)
        {
            double minimoAceito = numeroMinimo ?? 1;
            double maximoAceito = numeroMaximo ?? double.MaxValue;

            if (!source.Any())
                return minimoAceito;

            double maiorDaLista = source.Max(selector);

            if (maiorDaLista >= maximoAceito || maiorDaLista < minimoAceito)
                return minimoAceito;

            return maiorDaLista + 1;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int pageSize)
        {
            for (int i = 0; i < source.Count(); i += pageSize)
            {
                yield return source.ToList().GetRange(i, Math.Min(pageSize, source.Count() - i));
            }
        }
    }
}
