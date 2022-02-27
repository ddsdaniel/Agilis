namespace Agilis.Application.ViewModels.Seguranca
{
    /// <summary>
    /// Dados do usuário logado e token, retornado apenas em caso de sucesso
    /// </summary>
    public class UsuarioLogadoViewModel 
    {
        /// <summary>
        /// Usuário logado
        /// </summary>
        public UsuarioConsultaViewModel Usuario { get; set; }

        /// <summary>
        /// Token correspondente ao login
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Tipo do token
        /// </summary>
        public string TipoToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
