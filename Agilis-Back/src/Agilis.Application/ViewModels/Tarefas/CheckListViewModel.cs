using System;
using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class CheckListViewModel 
    {
        public Guid Id { get; set; }
        public string Nome { get;  set; }
        public IEnumerable<ItemCheckListViewModel> Itens { get;  set; }
        public int Ordem { get;  set; }
        public TarefaViewModel Tarefa { get;  set; }        
    }
}
