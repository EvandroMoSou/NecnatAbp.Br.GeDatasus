using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao.DmGoogleGeocoding
{
    public partial interface IGoogleGeocodingRepository : IRepository
    {
        Task<Logradouro?> GetLogradouroByCepAsync(string cep);
        Task<List<Logradouro>> SearchLogradouroByCidadeMunicipioIdAndNomeContainsAndNumeroAsync(Guid cidadeMunicipioId, string nomeContains, int? numero = null);
        Task<Geolocalizacao?> GetGeolocalizacaoByLogradouroAndNumeroAsync(Logradouro logradouro, int? numero);        
    }
}
