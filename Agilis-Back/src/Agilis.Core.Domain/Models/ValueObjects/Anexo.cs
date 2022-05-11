using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects
{
    public class Anexo : ValueObject<Anexo>
    {
        public Guid ArquivoId { get; private set; }
        public string Nome { get; private set; }
        public bool Imagem { get; private set; }//TODO: descobrir se é uma imagem

        protected Anexo() { }

        public Anexo(string nome, Guid arquivoId, bool imagem)
        {
            ArquivoId = arquivoId;
            Nome = nome;
            Imagem = imagem;
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
