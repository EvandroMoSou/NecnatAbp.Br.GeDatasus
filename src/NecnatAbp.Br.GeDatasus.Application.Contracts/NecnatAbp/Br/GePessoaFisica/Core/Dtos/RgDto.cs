using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class RgDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public string? Numero { get; set; }
        public DateTime? DataEmissao { get; set; }
        public Guid? OrgaoEmissorId { get; set; }
        public OrgaoEmissorDto? OrgaoEmissor { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
    }
}
