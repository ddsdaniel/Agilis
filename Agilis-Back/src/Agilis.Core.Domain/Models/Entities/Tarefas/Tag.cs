﻿using Agilis.Core.Domain.Abstractions.Models.Entities;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class Tag : Entidade
    {
        public string Nome { get; private set; }
        public IEnumerable<Tarefa> Tarefas { get; private set; }

        protected Tag() { }

        public Tag(string nome, IEnumerable<Tarefa> tarefas)
        {
            Nome = nome;
            Tarefas = tarefas;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome não deve ser nulo ou vazio");

            ImportarCriticas(Tarefas);
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}

