using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica.DmCbo
{
    public partial class Ocupacao : AuditedAggregateRoot<Guid>
    {
        public Guid FamiliaId { get; set; }
        public Familia? Familia { get; set; }
        public string CodigoCbo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
    }
}
