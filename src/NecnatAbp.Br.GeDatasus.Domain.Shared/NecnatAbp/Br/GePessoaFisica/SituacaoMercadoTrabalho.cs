using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum SituacaoMercadoTrabalho
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Empregador")]
        Empregador = 1,

        [Description("Assalariado Com Carteira de Trabalho")]
        AssalariadoComCarteiraTrabalho = 2,

        [Description("Assalariado Sem Carteira de Trabalho")]
        AssalariadoSemCarteiraTrabalho = 3,

        [Description("Autônomo Com Previdência Social")]
        AutonomoComPrevidenciaSocial = 4,

        [Description("Autônomo Sem Previdência Social")]
        AutonomoSemPrevidenciaSocial = 5,

        [Description("Servidor Público")]
        ServidorPublico = 6,

        [Description("Militar")]
        Militar = 7,

        [Description("Desempregado")]
        Desempregado = 8,

        [Description("Não Trabalha")]
        NaoTrabalha = 9,

        [Description("Outro")]
        Outro = 10,
    }
}
