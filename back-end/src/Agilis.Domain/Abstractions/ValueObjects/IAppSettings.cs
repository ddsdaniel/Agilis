namespace Agilis.Domain.Abstractions.ValueObjects
{
    /// <summary>
    /// Interface de configurações da aplicação, usada para viabilizar a injeção de dependências
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Chave privada, usada nos algoritmos de criptografia
        /// </summary>
        public string Segredo { get; set; }
    }
}
