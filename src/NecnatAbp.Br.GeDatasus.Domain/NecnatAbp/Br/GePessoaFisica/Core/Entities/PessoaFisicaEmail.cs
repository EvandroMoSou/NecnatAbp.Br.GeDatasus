using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaEmail : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public TipoPessoaFisicaEmail TipoPessoaFisicaEmail { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool RecebeNotificacoes { get; set; }
        public bool InPrincipal { get; set; }
    }
}
