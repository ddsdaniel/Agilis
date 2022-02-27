using AutoMapper;

namespace Agilis.Application.Extensions
{
    public static class IMapperExtensions
    {
        public static T Clone<T, TIntermediario>(this IMapper mapper, T objeto)
        {
            var intermediario = mapper.Map<TIntermediario>(objeto);
            var clone = mapper.Map<T>(intermediario);
            return clone;
        }
    }
}
