using Agilis.Domain.Enums;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Seguranca
{
    /// <summary>
    /// Classe usada nos métodos que retornam dados ao front
    /// </summary>
    public class UsuarioConsultaViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Email { get;  set; }
        public string Nome { get;  set; }
        public string Sobrenome { get;  set; }
        public RegraUsuario Regra { get; set; }
    }
}
