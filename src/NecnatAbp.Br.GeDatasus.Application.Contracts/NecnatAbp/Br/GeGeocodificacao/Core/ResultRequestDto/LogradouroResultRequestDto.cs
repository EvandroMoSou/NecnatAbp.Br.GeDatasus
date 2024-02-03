using NecnatAbp.Dtos;
using System;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial class LogradouroResultRequestDto : OptionalPagedAndSortedResultRequestDto
    {
        public string? GenericSearch { get; set; }
        public string? Cep { get; set; }
        public string? NomeContains { get; set; }
        public Guid? BairroDistritoId { get; set; }
        public Guid? CidadeMunicipioId { get; set; }
        public UnidadeFederativa? UnidadeFederativa { get; set; }
        public bool? InAtivo { get; set; }
    }
}
