using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Application.ViewModels.Seguranca
{
    public class UsuarioConsultaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public RegraUsuario Regra { get; set; }
    }
}
