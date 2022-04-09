using Agilis.Core.Domain.Abstractions.Models.Entities;

namespace Agilis.Core.Domain.Models.Entities
{
    public class Tarefa: Entidade
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }

        public override string ToString() => Titulo;
    }
}
