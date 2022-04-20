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
        public Guid FeatureId { get; private set; }
        public Feature Feature { get; private set; }
        public TipoTarefa Tipo { get; private set; }
        public Usuario Dev { get; private set; }
        public Guid DevId { get; private set; }
        public Usuario Tester { get; private set; }
        public Guid TesterId { get; private set; }
        public Usuario Analista { get; private set; }
        public Guid AnalistaId { get; private set; }

        protected Tarefa() { }

        public Tarefa(
            string titulo,
            string descricao,
            Guid featureId,
            Feature feature,
            TipoTarefa tipo,
            Usuario dev,
            Guid devId,
            Usuario tester,
            Guid testerId,
            Usuario analista,
            Guid analistaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            FeatureId = featureId;
            Feature = feature;
            Tipo = tipo;
            Dev = dev;
            DevId = devId;
            Tester = tester;
            TesterId = testerId;
            Analista = analista;
            AnalistaId = analistaId;
            Validar();
        }

        private void Validar()
        {
            if (String.IsNullOrEmpty(Titulo))
                Criticar("Título inválido.");

            if (Descricao == null)
                Criticar("Descrição inválida.");

            if (FeatureId == Guid.Empty)
                Criticar("Feature id inválido");
        }

        public override string ToString() => Titulo;
    }
}
