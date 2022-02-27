using Flunt.Notifications;

namespace Agilis.Core.Domain.Abstractions.Models.ValueObjects
{
    public abstract class ValueObject<T> : Notifiable  where T : ValueObject<T>
    {
        
    }
}