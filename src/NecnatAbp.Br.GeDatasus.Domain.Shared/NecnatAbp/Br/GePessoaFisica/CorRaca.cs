using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum CorRaca
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Branca")]
        Branca = 1,

        [Description("Preta")]
        Preta = 2,

        [Description("Parda")]
        Parda = 3,

        [Description("Amararela")]
        Amararela = 4,

        [Description("Indígena")]
        Indigena = 5,
    }
}