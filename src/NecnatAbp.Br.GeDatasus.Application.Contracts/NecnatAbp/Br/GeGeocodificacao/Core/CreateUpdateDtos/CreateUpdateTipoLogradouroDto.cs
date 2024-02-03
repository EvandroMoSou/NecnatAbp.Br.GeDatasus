using NecnatAbp.Dtos;
using System.ComponentModel.DataAnnotations;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CreateUpdateTipoLogradouroDto : ConcurrencyDto
    {
        public string? Sigla { get; set; }

        [Required]
        [StringLength(TipoLogradouroConsts.MaxNomeLength)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public bool InAtivo { get; set; }
    }
}
