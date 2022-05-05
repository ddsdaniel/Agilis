using System.Collections.Generic;

namespace Agilis.Application.ViewModels.Produtos
{
    public class EpicoViewModel
    {
        public string Nome { get; set; }
        public IEnumerable<FeatureViewModel> Features { get; set; }

    }
}
