using System;

namespace Agilis.WebAPI.ViewModels
{
    public abstract class BasicViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
