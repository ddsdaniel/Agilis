using Agilis.Core.Domain.Models.ValueObjects.Configuracoes;

namespace Agilis.Core.Domain.Abstractions.Models.ValueObjects
{
    /// <summary>
    /// Interface de configurações da aplicação, usada para viabilizar a injeção de dependências
    /// </summary>
    public interface IAppSettings
    {
        public BancoDadosSettings BancoDados { get; set; }
        public GoogleFcmAppSettings GoogleFcm { get; set; }
        public SmtpSettings Smtp { get; set; }
    }
}
