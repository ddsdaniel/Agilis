using System;

namespace Agilis.Application.ViewModels
{
    public class AnexoViewModel
    {
        public Guid ArquivoId { get; set; }
        public string Nome { get; set; }
        public string Base64 { get; set; }
        public bool Imagem { get; set; }
    }
}
