using Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects;

namespace Agilis.Infra.Configuracoes.Models.ValueObjects
{
    /// <summary>
    /// Classe de configurações da aplicação
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Chave privada, usada nos algoritmos de criptografia
        /// </summary>
        public string Segredo { get; set; }
        public GoogleFcmAppSettings GoogleFcm { get; set; }
        public SmtpSettings Smtp { get; set; }
    }
}
