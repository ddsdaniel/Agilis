namespace Agilis.Infra.Notifications.FirebaseCloudMessaging.Models.ValueObjects
{
    /// <summary>
    /// https://firebase.google.com/docs/cloud-messaging/http-server-ref
    /// </summary>
    public class GoogleFcmMessage
    {
        public GoogleFcmNotification Notification { get; set; }

        /// <summary>
        /// Este parâmetro especifica o destinatário de uma mensagem multicast, uma mensagem enviada a mais de um token de registro. O valor deve ser uma matriz de tokens de registro para onde enviar a mensagem multicast.A matriz precisa conter pelo menos 1 e no máximo 1.000 tokens de registro.Para enviar uma mensagem a um único dispositivo, use o parâmetro to. As mensagens multicast só são permitidas usando o formato HTTP JSON. Entre 1 e 1000.
        /// </summary>
        public string[] Registration_ids { get; set; }
    }
}
