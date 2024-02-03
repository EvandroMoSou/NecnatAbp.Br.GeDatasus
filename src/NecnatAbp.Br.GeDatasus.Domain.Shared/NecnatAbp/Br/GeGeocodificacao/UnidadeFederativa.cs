using System.ComponentModel;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public enum UnidadeFederativa
    {
        //Região Norte
        [Description("Rondônia")]
        RO = 11,
        [Description("Acre")]
        AC = 12,
        [Description("Amazonas")]
        AM = 13,
        [Description("Roraima")]
        RR = 14,
        [Description("Pará")]
        PA = 15,
        [Description("Amapá")]
        AP = 16,
        [Description("Tocantins")]
        TO = 17,

        //Região Nordeste
        [Description("Maranhão")]
        MA = 21,
        [Description("Piauí")]
        PI = 22,
        [Description("Ceará")]
        CE = 23,
        [Description("Rio Grande do Norte")]
        RN = 24,
        [Description("Paraíba")]
        PB = 25,
        [Description("Pernambuco")]
        PE = 26,
        [Description("Alagoas")]
        AL = 27,
        [Description("Sergipe")]
        SE = 28,
        [Description("Bahia")]
        BA = 29,

        //Região Sudeste
        [Description("Minas Gerais")]
        MG = 31,
        [Description("Espírito Santo")]
        ES = 32,
        [Description("Rio de Janeiro")]
        RJ = 33,
        [Description("São Paulo")]
        SP = 35,

        //Região Sul
        [Description("Paraná")]
        PR = 41,
        [Description("Santa Catarina")]
        SC = 42,
        [Description("Rio Grande do Sul")]
        RS = 43,

        //Região Centro-Oeste
        [Description("Mato Grosso do Sul")]
        MS = 50,
        [Description("Mato Grosso")]
        MT = 51,
        [Description("Goiás")]
        GO = 52,
        [Description("Distrito Federal")]
        DF = 53
    }
}
