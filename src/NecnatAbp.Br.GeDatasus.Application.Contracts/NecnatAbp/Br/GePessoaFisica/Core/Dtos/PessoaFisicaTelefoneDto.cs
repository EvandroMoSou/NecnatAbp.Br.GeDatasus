using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaTelefoneDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public TipoPessoaFisicaTelefone? TipoPessoaFisicaTelefone { get; set; }
        public string? Ddi { get; set; }
        public string? Ddd { get; set; }
        public string? Numero { get; set; }
        public string? NomeContato { get; set; }
        public bool? InPrincipal { get; set; }
        public bool? InRecebeNotificacoes { get; set; }
    }
}
