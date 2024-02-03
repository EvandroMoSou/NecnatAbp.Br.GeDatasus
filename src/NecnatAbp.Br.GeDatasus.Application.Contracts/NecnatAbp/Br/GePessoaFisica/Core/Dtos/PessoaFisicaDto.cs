using NecnatAbp.Br.GeGeocodificacao;
using NecnatAbp.Dtos;
using System;
using System.Collections.Generic;

namespace NecnatAbp.Br.GePessoaFisica
{
    public partial class PessoaFisicaDto : ConcurrencyAuditedEntityDto<Guid>
    {
        public Guid? UsuarioId { get; set; }
        public int? CodigoLegado { get; set; }

        //Identificacao
        public string? Nome { get; set; }
        public string? NomeFonetico { get; set; }
        public Sexo? Sexo { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? NomeSocial { get; set; }
        public string? NomeSocialFonetico { get; set; }

        //Filiacao
        public string? NomeMae { get; set; }
        public string? NomeMaeFonetico { get; set; }
        public bool? MaeDesconhecida { get; set; }
        public string? NomePai { get; set; }
        public string? NomePaiFonetico { get; set; }
        public bool? PaiDesconhecida { get; set; }

        //Nacionalidade
        public TipoNacionalidade? TipoNacionalidade { get; set; }
        public Guid? NacionalidadeId { get; set; }
        public PaisDto? NacionalidadeIdPais { get; set; }
        public Guid? NaturalidadeId { get; set; }
        public CidadeMunicipioDto? NaturalidadeIdCidadeMunicipio { get; set; }
        public Guid? PortariaNaturalizacaoId { get; set; }
        public PortariaNaturalizacaoDto? PortariaNaturalizacao { get; set; }
        public DateTime? DataNaturalizacao { get; set; }
        public DateTime? DataEntradaBrasil { get; set; }

        //SocioDemografica
        public CorRaca? CorRaca { get; set; }
        public Guid? EtniaId { get; set; }
        public EtniaDto? Etnia { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }
        public GrauInstrucao? GrauInstrucao { get; set; }
        public bool? GrauInstrucaoCursando { get; set; }
        //public Guid OcupacaoId { get; set; }
        //public Ocupacao? Ocupacao { get; set; }
        public SituacaoMercadoTrabalho? SituacaoMercadoTrabalho { get; set; }

        //Contato
        public ICollection<PessoaFisicaEmailDto>? ListaPessoaFisicaEmail { get; set; }
        public ICollection<PessoaFisicaTelefoneDto>? ListaPessoaFisicaTelefone { get; set; }
        public ICollection<PessoaFisicaEnderecoDto>? ListaPessoaFisicaEndereco { get; set; }

        //Documento
        public string? Cpf { get; set; }
        public ICollection<CertidaoDto>? ListaCertidao { get; set; }
        public ICollection<CnhDto>? ListaCnh { get; set; }
        public ICollection<CnsDto>? ListaCns { get; set; }
        public ICollection<CtpsDto>? ListaCtps { get; set; }
        public ICollection<DnvDto>? ListaDnv { get; set; }
        public ICollection<PassaporteDto>? ListaPassaporte { get; set; }
        public ICollection<PisPasepNisNitDto>? ListaPisPasepNisNit { get; set; }
        public ICollection<RicDto>? ListaRic { get; set; }
        public ICollection<RgDto>? ListaRg { get; set; }
        public ICollection<TituloEleitorDto>? ListaTituloEleitor { get; set; }

        //Obito
        public bool? InObito { get; set; }
        public DateTime? DataObito { get; set; }

        //Extra
        public string? Observacao { get; set; }
        public bool? InAtivo { get; set; }
    }
}
