using Agilis.Core.Domain.Abstractions.Models.Entities;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Sprint : Entidade
    {
        public string Nome { get; private set; }
        public string Objetivos { get; private set; }
        public DateTime? DataInicial { get; private set; }
        public DateTime? DataFinal { get; private set; }

        protected Sprint() { }

        public Sprint(string nome, DateTime? dataInicial, DateTime? dataFinal, string objetivos)
        {
            Nome = nome;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Objetivos = objetivos;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                Criticar("Nome inválido.");

            if (Objetivos == null)
                Criticar("Objetivos inválidos.");

            if (DataInicial.HasValue && DataFinal.HasValue && DataInicial.Value >= DataFinal.Value)
                Criticar("Data inicial deve ser menor que a data final.");
        }

        public override string ToString() => Nome;
    }
}
