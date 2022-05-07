using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects.Tarefas
{
    public class Comentario : ValueObject<Comentario>
    {
        public string Descricao { get; private set; }
        public Usuario Autor { get; private set; }
        public DateTime DataHora { get; private set; }

        public Comentario(string descricao, Usuario autor, DateTime dataHora)
        {
            Descricao = descricao;
            Autor = autor;
            DataHora = dataHora;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Descricao))
                Criticar("Descrição inválida");

            if (Autor == null)
                Criticar("Comentário anônimo");

            if (DataHora > DateTime.Now)
                Criticar("Data/hora futura");

            ImportarCriticas(Autor);
        }

        protected Comentario() { }

        public override string ToString() => Descricao;

    }
}
