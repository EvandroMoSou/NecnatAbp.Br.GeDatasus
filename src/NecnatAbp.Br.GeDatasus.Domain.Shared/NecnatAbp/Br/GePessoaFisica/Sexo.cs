using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum Sexo
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Masculino")]
        Masculino = 1,

        [Description("Feminino")]
        Feminino = 2,

        [Description("Intersexo")]
        Intersexo = 3
    }
}
