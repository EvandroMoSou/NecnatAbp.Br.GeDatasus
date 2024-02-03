using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class Logradouro : AuditedAggregateRoot<Guid>
    {
        public int Cep { get; set; }
        public Guid TipoLogradouroId { get; set; }
        public TipoLogradouro? TipoLogradouro { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? NomeAbreviado { get; set; }
        public string? Complemento { get; set; }
        public Guid BairroDistritoId { get; set; }
        public BairroDistrito? BairroDistrito { get; set; }
        public Guid CidadeMunicipioId { get; set; }
        public CidadeMunicipio? CidadeMunicipio { get; set; }
        public UnidadeFederativa UnidadeFederativa { get; set; }
        public Guid PaisId { get; set; }
        public Pais? Pais { get; set; }
        public bool InAtivo { get; set; }
        public int Origem { get; set; }
    }
}
