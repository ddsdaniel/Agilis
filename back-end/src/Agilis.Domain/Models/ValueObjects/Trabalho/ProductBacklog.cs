using Agilis.Domain.Enums;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class ProductBacklog : ValueObject<ProductBacklog>
    {
        public IEnumerable<Fase> Fases { get; private set; }
        public IEnumerable<PrioridadeProductBacklog> Prioridades { get; private set; }
        public IEnumerable<ItemProductBacklog> Itens { get; private set; }

        public ProductBacklog()
            :this(new List<Fase>(),
                  new List<PrioridadeProductBacklog>(),
                  new List<ItemProductBacklog>()
                 )
        {

        }

        public ProductBacklog(IEnumerable<Fase> fases,
                              IEnumerable<PrioridadeProductBacklog> prioridades,
                              IEnumerable<ItemProductBacklog> itens)
        {
            AddNotifications(new Contract()
                .IsNotNull(fases, nameof(Fases), "Lista de fases não deve ser nula")
                .IfNotNull(fases, c => c.Join(fases.ToArray()))
                
                .IsNotNull(prioridades, nameof(Prioridades), "Lista de prioridades não deve ser nula")

                .IsNotNull(itens, nameof(Itens), "Lista de itens não deve ser nula")
                .IfNotNull(itens, c => c.Join(itens.ToArray()))
                );

            Fases = fases;
            Prioridades = prioridades;
            Itens = itens;
        }

    }
}
