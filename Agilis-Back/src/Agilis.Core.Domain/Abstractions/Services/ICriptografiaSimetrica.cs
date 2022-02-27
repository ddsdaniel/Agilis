namespace Agilis.Core.Domain.Abstractions.Services
{
    public interface ICriptografiaSimetrica : IService
    {
        /// <summary>
        /// Criptografa a mensagem passada por parâmetro usando para isso o algoritmo e a sua chave secreta
        /// </summary>
        /// <param name="mensagem">Texto a ser criptografado</param>
        /// <param name="chaveSecreta">Chave secreta a ser usada pelo algortimo de criptografia</param>
        /// <returns>Mensagem cifrada resultante do algoritmo</returns>
        public abstract string Cifrar(string mensagem, string chaveSecreta);

        /// <summary>
        /// Criptografa a mensagem cifrada passada por parâmetro usando para isso o algoritmo e a sua chave secreta
        /// </summary>
        /// <param name="mensagemCifrada">Mensagem cifrada resultante do algoritmo</param>
        /// <param name="chaveSecreta">Chave secreta a ser usada pelo algortimo de criptografia</param>
        /// <returns>Mensagem decifrada resultante do algoritmo</returns>
        public abstract string Decifrar(string mensagemCifrada, string chaveSecreta);
    }
}
