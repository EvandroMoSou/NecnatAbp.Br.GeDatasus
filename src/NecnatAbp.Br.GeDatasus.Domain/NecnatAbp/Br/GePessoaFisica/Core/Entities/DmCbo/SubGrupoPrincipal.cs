using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial class SubGrupoPrincipal : AuditedAggregateRoot<Guid>
    {
        public Guid GrandeGrupoId { get; set; }
        public GrandeGrupo? GrandeGrupo { get; set; }
        public string CodigoCbo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
