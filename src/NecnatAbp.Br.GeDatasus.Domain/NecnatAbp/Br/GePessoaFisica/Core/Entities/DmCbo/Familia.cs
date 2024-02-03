using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial class Familia : AuditedAggregateRoot<Guid>
    {
        public Guid SubGrupoId { get; set; }
        public SubGrupo? SubGrupo { get; set; }
        public string CodigoCbo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
