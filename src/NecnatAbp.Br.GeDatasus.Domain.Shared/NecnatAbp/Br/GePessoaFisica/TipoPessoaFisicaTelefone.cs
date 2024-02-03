using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum TipoPessoaFisicaTelefone
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Celular")]
        Celular = 1,

        [Description("Residencial")]
        Residencial = 2,

        [Description("Comercial")]
        Comercial = 3,

        [Description("Contato")]
        Contato = 4,

        [Description("Emergência")]
        Emergencia = 5,

        [Description("Férias")]
        Ferias = 6,

        [Description("Bip/Pager")]
        BipPager = 7,

        [Description("Fax")]
        Fax = 8,
    }
}
