namespace NecnatAbp.Br.GeDatasus;

public static class GeDatasusDbProperties
{
    public static string DbTablePrefix { get; set; } = "GeDatasus";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "GeDatasus";
}
