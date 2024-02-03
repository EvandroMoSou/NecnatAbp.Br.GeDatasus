namespace NecnatAbp.Br.GeGeocodificacao
{
    public static partial class GeolocalizacaoUtil
    {
        public static string CepString(int cep)
        {
            return cep.ToString("00000000");
        }
    }
}
