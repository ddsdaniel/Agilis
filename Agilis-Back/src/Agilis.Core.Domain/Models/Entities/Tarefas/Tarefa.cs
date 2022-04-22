using Agilis.Core.Domain.Abstractions.Models.Entities;
using Agilis.Core.Domain.Enums;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using Agilis.Core.Domain.Models.ValueObjects;
using System.Collections.Generic;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class Tarefa : Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public Feature Feature { get; private set; }
        public TipoTarefa Tipo { get; private set; }
        public Usuario Relator { get; private set; }
        public Usuario Solucionador { get; private set; }
        public Hora HorasPrevistas { get; private set; }
        public Hora HorasRealizadas { get; private set; }
        public IEnumerable<Tag> Tags { get; private set; }

        protected Tarefa() { }

        public Tarefa(
            string titulo,
            string descricao,
            Feature feature,
            TipoTarefa tipo,
            Usuario relator,
            Usuario solucionador,
            Hora horasPrevistas,
            Hora horasRealizadas, 
            IEnumerable<Tag> tags)
        {
            //para evitar: System.InvalidOperationException: The instance of entity type 'Usuario' cannot be tracked because another instance with the key value '{Id: xyz}' is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
            if (relator?.Id == solucionador?.Id)
                relator = solucionador;

            Titulo = titulo;
            Descricao = descricao;
            Feature = feature;
            Tipo = tipo;
            Relator = relator;
            Solucionador = solucionador;
            HorasPrevistas = horasPrevistas;
            HorasRealizadas = horasRealizadas;
            Tags = tags;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (Feature == null)
                Criticar("Feature não deve ser nula");

            if (Relator == null)
                Criticar("Relator não deve ser nulo");

            ImportarCriticas(Feature);
            ImportarCriticas(HorasPrevistas);
            ImportarCriticas(HorasRealizadas);
            ImportarCriticas(Relator);
            ImportarCriticas(Solucionador);
            ImportarCriticas(Tags);
        }

        public override string ToString() => Titulo;
    }
}
