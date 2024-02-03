using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class CidadeMunicipioDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public UnidadeFederativa? UnidadeFederativa { get; set; }
        public string? Nome { get; set; }
        public string? CodigoIbge { get; set; }
        public bool? InAtivo { get; set; }
    }
}
