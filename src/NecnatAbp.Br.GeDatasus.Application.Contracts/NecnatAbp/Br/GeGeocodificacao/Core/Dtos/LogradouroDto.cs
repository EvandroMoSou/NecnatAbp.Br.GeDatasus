using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class LogradouroDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public string? Cep { get; set; }
        public string? Nome { get; set; }
        public string? NomeAbreviado { get; set; }
        public string? Complemento { get; set; }
        public Guid? TipoLogradouroId { get; set; }
        public TipoLogradouroDto? TipoLogradouro { get; set; }
        public Guid? BairroDistritoId { get; set; }
        public BairroDistritoDto? BairroDistrito { get; set; }
        public Guid? CidadeMunicipioId { get; set; }
        public CidadeMunicipioDto? CidadeMunicipio { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
        public Guid? PaisId { get; set; }
        public PaisDto? Pais { get; set; }
        public bool? InAtivo { get; set; }
    }
}