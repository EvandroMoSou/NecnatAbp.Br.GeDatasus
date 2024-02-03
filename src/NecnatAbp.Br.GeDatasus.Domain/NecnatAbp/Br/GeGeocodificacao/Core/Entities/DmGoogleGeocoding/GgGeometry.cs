using Newtonsoft.Json;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GgGeometry
    {
        [JsonProperty("bounds")]
        public GgRegion? Bounds { get; set; }

        [JsonProperty("location")]
        public GgLatLong? Location { get; set; }

        [JsonProperty("location_type")]
        public string? LocationType { get; set; }

        [JsonProperty("viewport")]
        public GgRegion? Viewport { get; set; }
    }
}
