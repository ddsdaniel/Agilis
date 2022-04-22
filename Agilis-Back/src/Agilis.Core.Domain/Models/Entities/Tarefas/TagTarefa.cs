using System;

namespace Agilis.Core.Domain.Models.Entities.Tarefas
{
    public class TagTarefa
    {
        public Tag Tag { get; private set; }
        public Guid TagId { get; private set; }

        public Tarefa Tarefa { get; private set; }
        public Guid TarefaId { get; private set; }

        protected TagTarefa() { }
        public TagTarefa(Tag tag, Guid tagId, Tarefa tarefa, Guid tarefaId)
        {
            Tag = tag;
            TagId = tagId;
            Tarefa = tarefa;
            TarefaId = tarefaId;
        }

    }
}
