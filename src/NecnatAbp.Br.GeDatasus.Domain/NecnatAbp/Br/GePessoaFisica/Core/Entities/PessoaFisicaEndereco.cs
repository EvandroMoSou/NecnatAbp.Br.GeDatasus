using NecnatAbp.Br.GeGeocodificacao;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaEndereco : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public Guid GeolocalizacaoId { get; set; }
        public Geolocalizacao? Geolocalizacao { get; set; }
        public TipoPessoaFisicaEndereco? TipoPessoaFisicaEndereco { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public bool InRecebeCorrespondencia { get; set; }
        public bool InPrincipal { get; set; }
    }
}
