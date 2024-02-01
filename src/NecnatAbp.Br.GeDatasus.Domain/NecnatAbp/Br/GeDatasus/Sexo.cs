using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeDatasus
{
    public class Sexo : AuditedAggregateRoot<Guid>
    {
        public int CodigoLediAps { get; set; }
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
