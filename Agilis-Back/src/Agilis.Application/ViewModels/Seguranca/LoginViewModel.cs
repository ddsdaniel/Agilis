namespace Agilis.Application.ViewModels.Seguranca
{
    /// <summary>
    /// Dados necessário para realizar o login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Endereço de e-mail para login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha para realizar o login
        /// </summary>
        public string Senha { get; set; }
    }
}
