using NecnatAbp.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CreateUpdateLogradouroDto : ConcurrencyDto
    {
        [Required]
        [StringLength(LogradouroConsts.MaxCepLength)]
        public string Cep { get; set; } = string.Empty;

        public Guid? TipoLogradouroId { get; set; }

        [Required]
        [StringLength(LogradouroConsts.MaxNomeLength)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(LogradouroConsts.MaxNomeAbreviadoLength)]
        public string? NomeAbreviado { get; set; }

        [StringLength(LogradouroConsts.MaxComplementoLength)]
        public string? Complemento { get; set; }

        [Required]
        public Guid BairroDistritoId { get; set; }

        [Required]
        public Guid CidadeMunicipioId { get; set; }

        [Required]
        public UnidadeFederativa? UnidadeFederativa { get; set; }

        [Required]
        public bool InAtivo { get; set; }
    }
}