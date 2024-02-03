using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum TipoCertidao
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Nascimento")]
        Nascimento = 1,

        [Description("Casamento")]
        Casamento = 2,

        [Description("Divórcio")]
        Divorcio = 3,

        [Description("Indígena")]
        Indigena = 4,

        [Description("Óbito")]
        Obito = 5,
    }
}
