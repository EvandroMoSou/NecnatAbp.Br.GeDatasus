using NecnatAbp.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class GeolocalizacaoResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? Cep { get; set; }
        public int? Numero { get; set; }
        public bool? InAtivo { get; set; }
    }
}
