using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeDatasus
{
    public class TipoNacionalidade : AuditedAggregateRoot<Guid>
    {
        public int CodigoLediAps { get; set; }
        public string CodigoCadSus { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
