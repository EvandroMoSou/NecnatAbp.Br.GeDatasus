using System.ComponentModel;

namespace NecnatAbp.Br.GePessoaFisica
{
    public enum GrauInstrucao
    {
        [Description("Não Informado")]
        NaoInformado = 0,

        [Description("Não Alfabetizado")]
        NaoAlfabetizado = 1,

        [Description("Alfabetizado")]
        Alfabetizado = 2,

        [Description("Ensino Fundamental I - Incompleto (1º a 5º Ano) (1ª a 4ª Série) (1º Grau)")]
        EnsinoFundamental1Incompleto = 3,

        [Description("Ensino Fundamental I - Completo (1º a 5º Ano) (1ª a 4ª Série) (1º Grau)")]
        EnsinoFundamental1Completo = 4,

        [Description("Ensino Fundamental II - Incompleto (6º a 9º Ano) (5ª a 8ª Série) (2º Grau)")]
        EnsinoFundamental2Incompleto = 5,

        [Description("Ensino Fundamental II - Completo (6º a 9º Ano) (5ª a 8ª Série) (2º Grau)")]
        EnsinoFundamental2Completo = 6,

        [Description("Ensino Médio - Incompleto (2º Grau)")]
        EnsinoMedioIncompleto = 7,

        [Description("Ensino Médio - Completo (2º Grau)")]
        EnsinoMedioCompleto = 8,

        [Description("Ensino Superior - Incompleto")]
        EnsinoSuperiorIncompleto = 9,

        [Description("Ensino Superior - Completo")]
        EnsinoSuperiorCompleto = 10,

        [Description("Mestrado")]
        Mestrado = 11,

        [Description("Doutorado")]
        Doutorado = 12,
    }
}
