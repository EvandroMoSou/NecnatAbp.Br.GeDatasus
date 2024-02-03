using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum TipoCns
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Definitivo")]
        Definitivo = 1,

        [Description("Provisório")]
        Provisorio = 2,
    }
}
