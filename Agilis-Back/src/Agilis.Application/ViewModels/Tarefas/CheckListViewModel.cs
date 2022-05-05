using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class CheckListViewModel 
    {
        public string Nome { get;  set; }
        public IEnumerable<ItemCheckListViewModel> Itens { get;  set; }
    }
}
