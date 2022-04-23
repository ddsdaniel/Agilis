using System;

namespace Agilis.Application.ViewModels.Tarefas
{
    public class ItemCheckListViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Concluido { get; set; }
        public string HorasPrevistas { get; set; }
        public int Ordem { get; set; }
        public CheckListViewModel CheckList { get; set; }

    }
}
