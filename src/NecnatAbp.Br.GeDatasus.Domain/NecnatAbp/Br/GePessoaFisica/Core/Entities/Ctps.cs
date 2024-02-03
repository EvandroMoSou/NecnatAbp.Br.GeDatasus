using NecnatAbp.Br.GeGeocodificacao;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class Ctps : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string? Serie { get; set; }
        public DateTime? DataEmissao { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
    }
}
