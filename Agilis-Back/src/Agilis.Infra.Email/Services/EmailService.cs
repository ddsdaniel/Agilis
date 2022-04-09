using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;
using Agilis.Infra.Emails.Abstractions.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Agilis.Infra.Emails.Services
{
    public class EmailService : Service, IEmailService
    {
        private readonly IAppSettings _appSettings;
        private readonly SmtpClient _smtpClient;

        public EmailService(
            IAppSettings appSettings,
            ICriptografiaSimetrica criptografiaSimetrica)
        {
            _appSettings = appSettings;

            var senha = criptografiaSimetrica.Decifrar(appSettings.Smtp.Senha, _appSettings.Segredo);

            _smtpClient = new SmtpClient
            {
                Host = _appSettings.Smtp.Host,
                Port = _appSettings.Smtp.Porta,
                EnableSsl = _appSettings.Smtp.HabilitarSsl,
                Timeout = _appSettings.Smtp.Timeout,
                Credentials = new NetworkCredential(_appSettings.Smtp.NomeUsuario, senha),
            };
        }

        public void Enviar(Email destinatario, string assunto, string mensagem, params string[] anexos)
        {
            try
            {
                if (destinatario == null)
                {
                    Criticar("Destinatário não deve ser nulo");
                    return;
                }
                ImportarCriticas(destinatario);

                if (String.IsNullOrEmpty(assunto))
                {
                    Criticar("Assunto não deve ser vazio ou nulo");
                    return;
                }

                if (String.IsNullOrEmpty(mensagem))
                {
                    Criticar("Mensagem não deve ser vazia ou nula");
                    return;
                }

                if (Invalido) return;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_appSettings.Smtp.Email, _appSettings.Smtp.NomeRemetente, Encoding.UTF8),
                    Subject = assunto,
                    Body = mensagem,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(destinatario.Endereco);

                if (anexos != null)
                {

                    if (anexos.Any(a => String.IsNullOrEmpty(a)))
                    {
                        Criticar("Anexo inválido");
                        return;
                    }

                    foreach (var anexo in anexos)
                        mailMessage.Attachments.Add(new Attachment(anexo));
                }

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                _smtpClient.Send(mailMessage);
            }
            catch (Exception exception)
            {
                Criticar(exception.Message);
            }
        }
    }
}
