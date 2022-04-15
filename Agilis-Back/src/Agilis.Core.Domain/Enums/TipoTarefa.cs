using System.ComponentModel;

namespace Agilis.Core.Domain.Enums
{
    public enum TipoTarefa
    {
        [Description("Melhoria")]
        Melhoria,

        [Description("Novidade")]
        Novidade,

        [Description("Bug")]
        Bug,

        [Description("Teste")]
        Teste,

        [Description("Qualificação")]
        Qualificacao
    }
}
