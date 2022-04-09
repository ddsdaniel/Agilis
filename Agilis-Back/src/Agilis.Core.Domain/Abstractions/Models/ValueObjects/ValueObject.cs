using DDS.Validacoes.Abstractions.Models;

namespace Agilis.Core.Domain.Abstractions.Models.ValueObjects
{
    public abstract class ValueObject<T> : Validavel  where T : ValueObject<T>
    {
        
    }
}