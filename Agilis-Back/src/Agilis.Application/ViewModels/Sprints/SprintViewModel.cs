using System;

namespace Agilis.Application.ViewModels.Sprints
{
    public class SprintViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Objetivos { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
