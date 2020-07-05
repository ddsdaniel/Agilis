using DDS.WebAPI.Abstractions.ViewModels;
using System;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels
{
    public class EntidadeNodoViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Rota { get; set; }
        public List<EntidadeNodoViewModel> Filhos { get; set; }
    }
}
