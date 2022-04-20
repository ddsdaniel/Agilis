using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using System;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Tarefa : Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Feature Feature { get; private set; }
        public TipoTarefa Tipo { get; private set; }
        public Usuario Relator { get; private set; }
        public Usuario Solucionador { get; private set; }

        protected Tarefa() { }

        public Tarefa(
            string titulo,
            string descricao,
            Feature feature,
            TipoTarefa tipo,
            Usuario relator,
            Usuario solucionador
            )
        {
            Titulo = titulo;
            Descricao = descricao;
            Feature = feature;
            Tipo = tipo;
            Relator = relator;
            Solucionador = solucionador;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (Feature == null)
                Criticar("Feature não deve ser nula");

            ImportarCriticas(Feature);
        }

        public override string ToString() => Titulo;
    }
}
