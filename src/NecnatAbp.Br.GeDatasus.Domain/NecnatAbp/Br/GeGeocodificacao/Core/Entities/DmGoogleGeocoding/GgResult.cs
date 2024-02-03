using Newtonsoft.Json;
using System.Collections.Generic;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GgResult
    {
        [JsonProperty("address_components")]
        public List<GgAddressComponent>? AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string? FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public GgGeometry? Geometry { get; set; }

        [JsonProperty("place_id")]
        public string? PlaceId { get; set; }

        [JsonProperty("postcode_localities")]
        public List<string>? PostcodeLocalities { get; set; }

        [JsonProperty("types")]
        public List<string>? Types { get; set; }
    }
}
