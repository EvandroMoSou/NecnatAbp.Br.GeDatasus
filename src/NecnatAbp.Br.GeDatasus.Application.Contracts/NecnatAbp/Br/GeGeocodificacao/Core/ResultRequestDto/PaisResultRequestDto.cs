using NecnatAbp.Dtos;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class PaisResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? GenericSearch { get; set; }
        public string? NomeContains { get; set; }
        public string? NomeInglesContains { get; set; }
        public string? NomeFrancesContains { get; set; }
        public string? CodigoIso3166Alpha2 { get; set; }
        public string? CodigoIso3166Alpha3 { get; set; }
        public string? CodigoIso3166Numeric { get; set; }
        public bool? InAtivo { get; set; }
    }
}
