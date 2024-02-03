using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class Pais : AuditedAggregateRoot<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public string? NomeIngles { get; set; }
        public string? NomeFrances { get; set; }
        public string? CodigoIso3166Alpha2 { get; set; }
        public string? CodigoIso3166Alpha3 { get; set; }
        public string? CodigoIso3166Numeric { get; set; }
        public bool InAtivo { get; set; }
        public int Origem { get; set; }
    }
}
