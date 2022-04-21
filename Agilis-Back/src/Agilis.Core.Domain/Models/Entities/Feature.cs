using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Feature : Entidade
    {
        public string Nome { get; private set; }
        public Epico Epico { get; private set; }
        public IEnumerable<Tarefa> Tarefas { get; private set; }

        protected Feature() { }

        public Feature(string nome, Epico epico, IEnumerable<Tarefa> tarefas)
        {
            Nome = nome;
            Epico = epico;
            Tarefas = tarefas;
            Validar();
        }


        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (Epico == null)
                Criticar("Épico não deve ser nulo.");

            ImportarCriticas(Epico);
        }

        public override string ToString() => Nome;
    }
}
