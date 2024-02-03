using Newtonsoft.Json;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GgRegion
    {
        [JsonProperty("northeast")]
        public GgLatLong? Northeast { get; set; }

        [JsonProperty("southwest")]
        public GgLatLong? Southwest { get; set; }
    }
}
