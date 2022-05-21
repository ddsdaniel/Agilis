using Agilis.Core.Domain.Enums;
using System;

namespace Agilis.Application.ViewModels
{
    public class AnexoViewModel
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public string Nome { get; set; }
        public TipoAnexo Tipo { get; set; }
    }
}
