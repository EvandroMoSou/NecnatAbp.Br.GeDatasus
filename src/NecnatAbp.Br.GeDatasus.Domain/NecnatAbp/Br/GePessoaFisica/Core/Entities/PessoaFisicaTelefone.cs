using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaTelefone : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public TipoPessoaFisicaTelefone TipoPessoaFisicaTelefone { get; set; }
        public string? Ddi { get; set; }
        public string? Ddd { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string? NomeContato { get; set; }
        public bool InPrincipal { get; set; }
        public bool InRecebeNotificacoes { get; set; }
    }
}
