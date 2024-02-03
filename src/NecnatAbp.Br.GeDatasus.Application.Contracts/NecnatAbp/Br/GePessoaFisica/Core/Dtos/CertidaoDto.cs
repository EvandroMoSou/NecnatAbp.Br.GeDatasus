using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class CertidaoDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? PessoaFisicaId { get; set; }
        public TipoCertidao? TipoCertidao { get; set; }
        public ModeloCertidao? ModeloCertidao { get; set; }
        public DateTime? DataEmissao { get; set; }

        //Modelo Antigo
        public string? Livro { get; set; }
        public string? Folha { get; set; }
        public string? Termo { get; set; }
        public string? NomeCartorio { get; set; }

        //Modelo Novo
        public string? Matricula { get; set; }
    }
}
