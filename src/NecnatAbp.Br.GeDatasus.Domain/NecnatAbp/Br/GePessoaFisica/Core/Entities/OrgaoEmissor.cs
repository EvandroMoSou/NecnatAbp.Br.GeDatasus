using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class OrgaoEmissor : AuditedAggregateRoot<Guid>
    {
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public bool InAtivo { get; set; }
    }
}
