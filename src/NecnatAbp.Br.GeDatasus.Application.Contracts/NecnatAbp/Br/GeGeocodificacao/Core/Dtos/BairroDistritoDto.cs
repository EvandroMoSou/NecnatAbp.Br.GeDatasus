using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class BairroDistritoDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? CidadeMunicipioId { get; set; }
        public string? Nome { get; set; }
        public string? CodigoIbge { get; set; }
        public bool? InAtivo { get; set; }

        public SubdistritoDto? Subdistrito { get; set; }
    }
}
