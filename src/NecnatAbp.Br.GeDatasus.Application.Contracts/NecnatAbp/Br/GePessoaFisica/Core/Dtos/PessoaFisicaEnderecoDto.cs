using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaEnderecoDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public Guid? GeolocalizacaoId { get; set; }
        public GeolocalizacaoDto? Geolocalizacao { get; set; }
        public TipoPessoaFisicaEndereco? TipoPessoaFisicaEndereco { get; set; }
        public string? Complemento { get; set; }
        public string? Referencia { get; set; }
        public bool? InRecebeCorrespondencia { get; set; }
        public bool? InPrincipal { get; set; }
    }
}
