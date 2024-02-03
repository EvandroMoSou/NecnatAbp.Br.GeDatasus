using Newtonsoft.Json;
using System.Collections.Generic;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial class GgResponse
    {
        [JsonProperty("results")]
        public List<GgResult>? Results { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
