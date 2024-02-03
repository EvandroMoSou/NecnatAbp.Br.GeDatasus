using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum TipoNacionalidade
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Brasileiro")]
        Brasileiro = 1,

        [Description("Naturalizado")]
        Naturalizado = 2,

        [Description("Estrangeiro")]
        Estrangeiro = 3
    }
}
