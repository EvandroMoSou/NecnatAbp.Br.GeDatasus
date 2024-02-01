namespace NecnatAbp.Br.GeDatasus.Converters
{
    public interface IPaisConverter
    {
        GeGeocodificacao.PaisDto FromCodigoCadSus(string codigoEtnia);
    }
}
