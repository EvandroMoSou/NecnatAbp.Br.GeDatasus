using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PassaporteDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public string? Numero { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataValidade { get; set; }
        public Guid? PaisIdEmissao { get; set; }
        public PaisDto? PaisEmissao { get; set; }
    }
}
