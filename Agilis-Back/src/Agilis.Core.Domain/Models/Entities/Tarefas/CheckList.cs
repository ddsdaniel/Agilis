using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class CheckList : Entidade
    {
        public string Nome { get; private set; }
        public IEnumerable<ItemCheckList> Itens { get; private set; }
        public int Ordem { get; private set; }
        public Tarefa Tarefa { get; private set; }

        protected CheckList() { }

        public CheckList(
            string nome,
            IEnumerable<ItemCheckList> itens,
            int ordem,
            Tarefa tarefa)
        {
            Nome = nome;
            Itens = itens;
            Ordem = ordem;
            Tarefa = tarefa;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");

            if (Itens == null)
                Criticar("Itens não deve ser nulo");

            if (Ordem <= 0)
                Criticar("Ordem deve ser maior que zero");

            ImportarCriticas(Itens);
            ImportarCriticas(Tarefa);
        }

        public override string ToString() => Nome;
    }
}
