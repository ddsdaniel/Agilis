using DDS.WebAPI.Abstractions.ViewModels;
using System.Collections.Generic;

namespace Agilis.WebAPI.ViewModels.Trabalho
{
    public class StoryMappingViewModel : IViewModel
    {
        public IEnumerable<TemaViewModel> Temas { get; set; }
    }
}
