using NecnatAbp.Br.GeGeocodificacao;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisica : AuditedAggregateRoot<Guid>
    {
        public Guid? UsuarioId { get; set; }
        public int? CodigoLegado { get; set; }

        //Identificacao
        public string Nome { get; set; } = string.Empty;
        public string NomeFonetico { get; set; } = string.Empty;
        public Sexo Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? NomeSocial { get; set; }
        public string? NomeSocialFonetico { get; set; }

        //Filiacao
        public string? NomeMae { get; set; }
        public string? NomeMaeFonetico { get; set; }
        public bool MaeDesconhecida { get; set; }
        public string? NomePai { get; set; }
        public string? NomePaiFonetico { get; set; }
        public bool? PaiDesconhecida { get; set; }

        //Nacionalidade
        public TipoNacionalidade? TipoNacionalidade { get; set; }
        public Guid? NacionalidadeId { get; set; }
        public Pais? NacionalidadeIdPais { get; set; }
        public Guid? NaturalidadeId { get; set; }
        public CidadeMunicipio? NaturalidadeIdCidadeMunicipio { get; set; }
        public Guid? PortariaNaturalizacaoId { get; set; }
        public PortariaNaturalizacao? PortariaNaturalizacao { get; set; }
        public DateTime? DataNaturalizacao { get; set; }
        public DateTime? DataEntradaBrasil { get; set; }

        //SocioDemografica
        public CorRaca? CorRaca { get; set; }
        public Guid? EtniaId { get; set; }
        public Etnia? Etnia { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public GrauInstrucao? GrauInstrucao { get; set; }
        public bool? GrauInstrucaoCursando { get; set; }
        //public Guid OcupacaoId { get; set; }
        //public Ocupacao? Ocupacao { get; set; }
        public SituacaoMercadoTrabalho? SituacaoMercadoTrabalho { get; set; }

        //Contato
        public ICollection<PessoaFisicaEmail>? ListaPessoaFisicaEmail { get; set; }
        public ICollection<PessoaFisicaTelefone>? ListaPessoaFisicaTelefone { get; set; }
        public ICollection<PessoaFisicaEndereco>? ListaPessoaFisicaEndereco { get; set; }

        //Documento
        public string? Cpf { get; set; }
        public ICollection<Certidao>? ListaCertidao { get; set; }
        public ICollection<Cnh>? ListaCnh { get; set; }
        public ICollection<Cns>? ListaCns { get; set; }
        public ICollection<Ctps>? ListaCtps { get; set; }
        public ICollection<Dnv>? ListaDnv { get; set; }
        public ICollection<Passaporte>? ListaPassaporte { get; set; }
        public ICollection<PisPasepNisNit>? ListaPisPasepNisNit { get; set; }
        public ICollection<Ric>? ListaRic { get; set; }
        public ICollection<Rg>? ListaRg { get; set; }
        public ICollection<TituloEleitor>? ListaTituloEleitor { get; set; }

        //Obito
        public bool? InObito { get; set; }
        public DateTime? DataObito { get; set; }

        //Extra
        public string? Observacao { get; set; }
        public bool InAtivo { get; set; }
    }
}
