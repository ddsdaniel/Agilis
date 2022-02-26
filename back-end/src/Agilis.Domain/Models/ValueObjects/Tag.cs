using DDS.Domain.Core.Abstractions.Models.ValueObjects;

namespace Agilis.Domain.Models.ValueObjects
{
    public class Tag : ValueObject<Tag>
    {
        public string Nome { get; private set; }
    }
}
