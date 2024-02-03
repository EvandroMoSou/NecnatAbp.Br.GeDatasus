using Newtonsoft.Json;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GgLatLong
    {
        [JsonProperty("lat")]
        public decimal? Latitude { get; set; }

        [JsonProperty("lng")]
        public decimal? Longitude { get; set; }
    }
}
