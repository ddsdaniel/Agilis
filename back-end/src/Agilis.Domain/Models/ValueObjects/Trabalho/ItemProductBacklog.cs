using Agilis.Domain.Abstractions.Entities.Trabalho;
using Agilis.Domain.Enums;
using DDS.Domain.Core.Abstractions.Model.ValueObjects;
using Flunt.Validations;

namespace Agilis.Domain.Models.ValueObjects.Trabalho
{
    public class ItemProductBacklog : ValueObject<ItemProductBacklog>
    {
        public Tarefa Tarefa { get; private set; }
        public Fase Fase { get; private set; }
        public PrioridadeProductBacklog Prioridade { get; private set; }
        public int Posicao { get; private set; }

        public ItemProductBacklog(Tarefa tarefa,
                                  Fase fase,
                                  PrioridadeProductBacklog prioridade,
                                  int posicao)
        {
            AddNotifications(new Contract()
                .IsNotNull(tarefa, nameof(Tarefa), "Tarefa não deve ser nula")
                .IfNotNull(tarefa, c => c.Join(tarefa))
                .IsNotNull(fase, nameof(Fase), "Fase não deve ser nula")
                .IfNotNull(fase, c => c.Join(fase))
                .IsGreaterThan(posicao, 0, nameof(Posicao), "Posição deve ser maior que zero")
                );

            Tarefa = tarefa;
            Fase = fase;
            Prioridade = prioridade;
            Posicao = posicao;
        }
    }
}
