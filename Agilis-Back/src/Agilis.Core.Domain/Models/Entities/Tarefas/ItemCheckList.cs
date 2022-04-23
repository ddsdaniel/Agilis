using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class ItemCheckList : Entidade
    {
        public string Nome { get; private set; }
        public bool Concluido { get; private set; }
        public Hora HorasPrevistas { get; private set; }
        public int Ordem { get; private set; }
        public CheckList CheckList { get; private set; }

        protected ItemCheckList() { }

        public ItemCheckList(
            string nome,
            bool concluido,
            Hora horasPrevistas,
            int ordem,
            CheckList checkList)
        {
            Nome = nome;
            Concluido = concluido;
            HorasPrevistas = horasPrevistas;
            Ordem = ordem;
            CheckList = checkList;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");

            if (Ordem <= 0)
                Criticar("Ordem deve ser maior que zero");

            ImportarCriticas(HorasPrevistas);
            ImportarCriticas(CheckList);
        }

        public override string ToString() => Nome;
    }
}
