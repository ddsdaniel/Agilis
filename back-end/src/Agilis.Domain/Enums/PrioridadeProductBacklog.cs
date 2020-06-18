using System.ComponentModel;

namespace Agilis.Domain.Enums
{
    public enum PrioridadeProductBacklog
    {
        [Description("Caixa de Entrada")]
        CaixaEntrada = 1,

        [Description("Próximo Sprint")]
        ProximoSprint = 2,

        [Description("Próximos dois Sprints")]
        ProximoParSprints = 3,

        [Description("Sprints Futuros")]
        SprintsFuturos = 4
    }
}
