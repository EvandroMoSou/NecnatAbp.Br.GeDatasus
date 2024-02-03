using NecnatAbp.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CreateUpdateSubdistritoDto : ConcurrencyDto
    {
        [Required]
        public Guid BairroDistritoId { get; set; }

        [Required]
        [StringLength(SubdistritoConsts.MaxNomeLength)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(SubdistritoConsts.MaxCodigoIbgeLength)]
        public string? CodigoIbge { get; set; }

        [Required]
        public bool InAtivo { get; set; }
    }
}
