using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class TituloEleitorDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public string? Numero { get; set; }
        public string? Zona { get; set; }
        public string? Secao { get; set; }
        public DateTime? DataEmissao { get; set; }
    }
}
