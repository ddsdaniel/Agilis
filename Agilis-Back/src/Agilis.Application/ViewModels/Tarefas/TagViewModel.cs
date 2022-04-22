using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public IEnumerable<TarefaViewModel> Tarefas { get; set; }
    }
}
