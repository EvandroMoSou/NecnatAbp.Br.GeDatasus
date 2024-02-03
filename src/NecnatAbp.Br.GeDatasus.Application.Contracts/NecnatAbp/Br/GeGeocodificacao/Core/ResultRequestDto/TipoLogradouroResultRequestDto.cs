using NecnatAbp.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class TipoLogradouroResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? Sigla { get; set; }
        public string? NomeContains { get; set; }
        public bool? InAtivo { get; set; }
    }
}
