using Agilis.Core.Domain.Models.ValueObjects;
using System.Threading.Tasks;

namespace Agilis.Core.Domain.Abstractions.Services
{
    public interface INotificationService
    {
        Task NotificarAsync(Notificacao notificacao);
    }
}
