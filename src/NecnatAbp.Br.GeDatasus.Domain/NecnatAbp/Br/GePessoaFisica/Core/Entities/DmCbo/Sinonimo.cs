using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial class Sinonimo : AuditedAggregateRoot<Guid>
    {
        public Guid OcupacaoId { get; set; }
        public Ocupacao? Ocupacao { get; set; }
        public string CodigoCbo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
