using Agilis.Infra.Configuracoes.Models.ValueObjects;

namespace Agilis.Infra.Configuracoes.Abstractions.Models.ValueObjects
{
    /// <summary>
    /// Interface de configurações da aplicação, usada para viabilizar a injeção de dependências
    /// </summary>
    public interface IAppSettings
    {
        //TODO: mover pra infra


        /// <summary>
        /// Chave privada, usada nos algoritmos de criptografia
        /// </summary>
        public string Segredo { get; set; }

        public GoogleFcmAppSettings GoogleFcm { get; set; }
        public SmtpSettings Smtp { get; set; }
    }
}
