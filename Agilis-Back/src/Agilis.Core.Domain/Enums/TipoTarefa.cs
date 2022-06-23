using System.ComponentModel;

namespace Agilis.Core.Domain.Enums
{
    public enum TipoTarefa
    {
        [Description("Não identificado")]
        NaoIdentificado,

        [Description("Melhoria")]
        Melhoria,

        //TODO: feature?
        [Description("Novidade")]
        Novidade,

        [Description("Bug")]
        Bug,

        [Description("Teste")]
        Teste,

        [Description("Qualificação")]
        Qualificacao
            //TODO: débito técnico
    }
}
