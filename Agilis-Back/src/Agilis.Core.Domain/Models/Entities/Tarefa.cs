using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Tarefa : Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        protected Tarefa() { }

        public Tarefa(string titulo, string descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");
        }

        public override string ToString() => Titulo;
    }
}
