using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeDatasus
{
    public class TipoCertidao : AuditedAggregateRoot<Guid>
    {
        public string CodigoCadSus { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
