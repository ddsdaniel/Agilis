using Agilis.Domain.Abstractions.ValueObjects;

namespace Agilis.Domain.Models.ValueObjects
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
    }
}
