using DDS.WebAPI.Abstractions.ViewModels;

namespace Agilis.WebAPI.ViewModels
{
    public class OrigemDestinoViewModel:IViewModel
    {
        public int Origem { get; set; }
        public int Destino { get; set; }
    }
}
