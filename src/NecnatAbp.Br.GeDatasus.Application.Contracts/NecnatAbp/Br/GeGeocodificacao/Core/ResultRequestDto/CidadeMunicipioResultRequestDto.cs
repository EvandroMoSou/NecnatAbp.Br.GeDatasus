using NecnatAbp.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CidadeMunicipioResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? GenericSearch { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
        public string? NomeContains { get; set; }
        public string? CodigoIbge { get; set; }
        public bool? InAtivo { get; set; }
    }
}
