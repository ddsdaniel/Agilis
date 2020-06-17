using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels
{
    public class IntervaloDatasViewModel : IViewModel
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
