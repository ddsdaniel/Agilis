using Agilis.Application.ViewModels.Tarefas;
using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Produtos
{
    public class FeatureViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid EpicoId { get; set; }
        public EpicoViewModel Epico { get; set; }
        public IEnumerable<TarefaViewModel> Tarefas { get; set; }
    }
}
