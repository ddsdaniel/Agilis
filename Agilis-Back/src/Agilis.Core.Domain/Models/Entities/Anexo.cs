using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Anexo : Entidade
    {
        public string Conteudo { get; private set; }
        public string Nome { get; private set; }

        public TipoAnexo Tipo
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

                if (imagens.Contains(Path.GetExtension(Nome).ToLower()))
                    return TipoAnexo.Imagem;

                if (new Url(Conteudo).Valido)
                    return TipoAnexo.Link;

                return TipoAnexo.Arquivo;

                //TODO: tratar demais tipos
            }
        }

        protected Anexo() { }

        public Anexo(string conteudo, string nome)
        {
            Conteudo = conteudo;
            Nome = nome;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(Conteudo))
                Criticar("Conteúdo inválido");

            if (String.IsNullOrWhiteSpace(Nome))
                Criticar("Nome inválido");
        }

        public override string ToString() => Id.ToString();
    }
}
