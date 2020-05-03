using DDS.WebAPI.Abstractions.ViewModels;

namespace Agilis.WebAPI.ViewModels.Seguranca
{
    /// <summary>
    /// Classe que repsenta a requisição de troca de senha do usuário
    /// </summary>
    public class AlteraSenhaViewModel : IViewModel
    {
        /// <summary>
        /// Senha atual do usuário
        /// </summary>
        public string SenhaAtual { get; set; }

        /// <summary>
        /// Nova senha desejada
        /// </summary>
        public string NovaSenha { get; set; }

        /// <summary>
        /// Confirmação da senha para evitar erros de digitação
        /// </summary>
        public string ConfirmaSenha { get; set; }
    }
}
