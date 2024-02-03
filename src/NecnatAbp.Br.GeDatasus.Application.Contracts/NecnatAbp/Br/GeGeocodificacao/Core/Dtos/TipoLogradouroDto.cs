using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class TipoLogradouroDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public string? Sigla { get; set; }
        public string? Nome { get; set; }
        public bool InAtivo { get; set; }
    }
}