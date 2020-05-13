using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;

namespace Agilis.WebAPI.ViewModels.Seguranca
{
    /// <summary>
    /// Dados do usuário logado e token, retornado apenas em caso de sucesso
    /// </summary>
    public class UsuarioLogadoViewModel : IViewModel
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
    }
}
