using Agilis.Core.Domain.Abstractions.Models.ValueObjects;
using Agilis.Core.Domain.Models.ValueObjects;
using System;

namespace Agilis.Core.Domain.Models.ValueObjects.CheckLists
{
    public class ItemCheckList : ValueObject<ItemCheckList>
    {
        public string Nome { get; private set; }
        public bool Concluido { get; private set; }
        public Hora HorasPrevistas { get; private set; }

        protected ItemCheckList() { }

        public ItemCheckList(
            string nome,
            bool concluido,
            Hora horasPrevistas
            )
        {
            Nome = nome;
            Concluido = concluido;
            HorasPrevistas = horasPrevistas;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                Criticar("Nome inválido");

            ImportarCriticas(HorasPrevistas);
        }

        public override string ToString() => Nome;
    }
}
