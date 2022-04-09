namespace Agilis.Infra.Notifications.FirebaseCloudMessaging.Models.ValueObjects
{
    /// <summary>
    /// Este parâmetro especifica os pares de chave-valor personalizados do payload da mensagem. É recomendável usar valores do tipo string. É preciso converter os valores nos objetos ou outros tipos de dados sem string (por exemplo, números inteiros ou booleanos) para string.
    /// </summary>
    public class GoogleFcmNotification
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Icon { get; set; }
        public string Click_action { get; set; }
    }
}
