using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Core.Domain.Models.ValueObjects.Configuracoes
{
    /// <summary>
    /// Classe de configurações da aplicação
    /// </summary>
    public class AppSettings : IAppSettings
    {
        public string Segredo { get; set; }
        public GoogleFcmAppSettings GoogleFcm { get; set; }
        public SmtpSettings Smtp { get; set; }
        public BancoDadosSettings BancoDados { get; set; }
    }
}
