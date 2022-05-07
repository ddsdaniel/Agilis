using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.ValueObjects.Tarefas
{
    public class CheckList : ValueObject<CheckList>
    {
        public string Nome { get; private set; }
        public IEnumerable<ItemCheckList> Itens { get; private set; }

        protected CheckList() { }

        public CheckList(
            string nome,
            IEnumerable<ItemCheckList> itens
            )
        {
            Nome = nome;
            Itens = itens;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");

            if (Itens == null)
                Criticar("Itens não deve ser nulo");

            ImportarCriticas(Itens);
        }

        public override string ToString() => Nome;
    }
}
