using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum TipoPessoaFisicaEndereco
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Pessoal")]
        Pessoal = 1,

        [Description("Comercial")]
        Comercial = 2,

        [Description("Correspondência")]
        Correspondencia = 3,
    }
}
