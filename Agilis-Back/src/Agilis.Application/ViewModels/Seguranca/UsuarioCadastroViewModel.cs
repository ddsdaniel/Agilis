using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Application.ViewModels.Seguranca
{
    public class UsuarioCadastroViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public RegraUsuario Regra { get; set; }
    }
}
