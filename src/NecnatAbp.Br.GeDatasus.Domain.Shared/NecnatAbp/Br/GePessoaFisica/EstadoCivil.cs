using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum EstadoCivil
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Solteiro")]
        Solteiro = 1, //UniaoEstavel

        [Description("Casado")]
        Casado = 2,

        [Description("Divorciado")]
        Divorciado = 3,

        [Description("Viúvo")]
        Viuvo = 4,

        [Description("Separado Judicialmente / Desquitado")]
        SeparadoJudicialmente = 5,
    }
}
