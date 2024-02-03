using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class Certidao : AuditedAggregateRoot<Guid>, IPessoaFisicaUnificacao<Guid>
    {
        public Guid PessoaFisicaId { get; set; }
        public PessoaFisica? PessoaFisica { get; set; }
        public Guid? PessoaFisicaIdUnificacao { get; set; }
        public TipoCertidao TipoCertidao { get; set; }
        public ModeloCertidao ModeloCertidao { get; set; }
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
