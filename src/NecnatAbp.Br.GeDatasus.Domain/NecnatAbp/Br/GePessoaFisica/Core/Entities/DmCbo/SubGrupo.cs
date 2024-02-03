using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial class SubGrupo : AuditedAggregateRoot<Guid>
    {
        public Guid SubGrupoPrincipalId { get; set; }
        public SubGrupoPrincipal? SubGrupoPrincipal { get; set; }
        public string CodigoCbo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
