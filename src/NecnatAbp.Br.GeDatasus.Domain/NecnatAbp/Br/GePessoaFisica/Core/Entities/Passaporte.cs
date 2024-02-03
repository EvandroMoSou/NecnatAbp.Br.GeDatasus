using NecnatAbp.Br.GeGeocodificacao;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class Passaporte : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public string Numero { get; set; } = string.Empty;
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataValidade { get; set; }
        public Guid? PaisIdEmissao { get; set; }
        public Pais? PaisEmissao { get; set; }
    }
}
