using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class BairroDistrito : AuditedAggregateRoot<Guid>
    {
        public Guid CidadeMunicipioId { get; set; }
        public CidadeMunicipio? CidadeMunicipio { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? CodigoIbge { get; set; }
        public bool InAtivo { get; set; }
        public int Origem { get; set; }
    }
}
