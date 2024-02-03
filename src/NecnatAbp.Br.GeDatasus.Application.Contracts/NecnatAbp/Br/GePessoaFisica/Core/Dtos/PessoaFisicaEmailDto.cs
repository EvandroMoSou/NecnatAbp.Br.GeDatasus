using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaEmailDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public TipoPessoaFisicaEmail? TipoPessoaFisicaEmail { get; set; }
        public string? Email { get; set; }
        public bool? RecebeNotificacoes { get; set; }
        public bool? InPrincipal { get; set; }
    }
}
