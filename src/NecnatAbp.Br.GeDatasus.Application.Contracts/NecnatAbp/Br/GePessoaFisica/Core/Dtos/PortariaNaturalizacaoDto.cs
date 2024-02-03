using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PortariaNaturalizacaoDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public string? Nome { get; set; }
    }
}
