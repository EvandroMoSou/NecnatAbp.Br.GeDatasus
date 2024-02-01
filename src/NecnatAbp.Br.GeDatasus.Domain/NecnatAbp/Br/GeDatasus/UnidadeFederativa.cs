using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeDatasus
{
    public class UnidadeFederativa : AuditedAggregateRoot<Guid>
    {
        public string CodigoLediAps { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
