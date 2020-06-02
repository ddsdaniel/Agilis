using Agilis.WebAPI.ViewModels.Pessoas;
using DDS.WebAPI.Abstractions.ViewModels;
using System;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    public class ReleaseViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public TimeViewModel Time { get; set; }
    }
}
