using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class SubdistritoDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? BairroDistritoId { get; set; }
        public string? Nome { get; set; }
        public string? CodigoIbge { get; set; }
        public bool? InAtivo { get; set; }
    }
}
