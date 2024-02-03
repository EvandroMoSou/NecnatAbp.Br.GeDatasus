using NecnatAbp.Dtos;
using System.ComponentModel.DataAnnotations;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CreateUpdatePaisDto : ConcurrencyDto
    {
        [Required]
        [StringLength(PaisConsts.MaxNomeLength)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(PaisConsts.MaxNomeInglesLength)]
        public string? NomeIngles { get; set; }

        [StringLength(PaisConsts.MaxNomeFrancesLength)]
        public string? NomeFrances { get; set; }

        [StringLength(PaisConsts.MaxCodigoIso3166Alpha2Length)]
        public string? CodigoIso3166Alpha2 { get; set; }

        [StringLength(PaisConsts.MaxCodigoIso3166Alpha3Length)]
        public string? CodigoIso3166Alpha3 { get; set; }

        [StringLength(PaisConsts.MaxCodigoIso3166NumericLength)]
        public string? CodigoIso3166Numeric { get; set; }

        [Required]
        public bool InAtivo { get; set; }
    }
}
