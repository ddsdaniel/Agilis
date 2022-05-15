using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Anexo : ValueObject<Anexo>
    {
        public Guid ArquivoId { get; private set; }
        public string Nome { get; private set; }
        public bool Imagem
        {
            get
            {
                var imagens = new List<string>
                {
                    ".apng",
                    ".avif",
                    ".gif",
                    ".jpg",
                    ".jpeg",
                    ".jfif",
                    ".pjpeg",
                    ".pjp",
                    ".png",
                    ".svg",
                    ".webp"
                };

                return imagens.Contains(Path.GetExtension(Nome).ToLower());
            }
        }

        protected Anexo() { }

        public Anexo(string nome, Guid arquivoId)
        {
            ArquivoId = arquivoId;
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (ArquivoId == Guid.Empty)
                Criticar("ArquivoId inválido");

            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");
        }

        public override string ToString() => Nome;
    }
}
