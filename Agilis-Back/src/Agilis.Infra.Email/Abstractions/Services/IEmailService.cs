using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Models.ValueObjects;

namespace Agilis.Infra.Emails.Abstractions.Services
{
    public interface IEmailService : IService
    {
        void Enviar(Email destinatario, string assunto, string mensagem, params string[] anexos);
    }
}