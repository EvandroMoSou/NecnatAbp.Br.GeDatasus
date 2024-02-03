using System.ComponentModel;

namespace NecnatAbp.Br.GeDatasus
{
    public enum TipoSanguineo
    {
        [Description("A+")]
        APositivo,

        [Description("A-")]
        ANegativo,

        [Description("AB+")]
        ABPositivo,

        [Description("AB-")]
        ABNegativo,

        [Description("B+")]
        BPositivo,

        [Description("B-")]
        BNegativo,

        [Description("O+")]
        OPositivo,

        [Description("O-")]
        ONegativo
    }
}