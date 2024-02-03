using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NecnatAbp.Br.GeGeocodificacao
{
    public partial interface IGeolocalizacaoRepository : IRepository<Geolocalizacao, Guid>
    {
        Task<Geolocalizacao?> GetByCepAndNumeroWithLogradouroAsync(int cep, int? numero);
        Task<Geolocalizacao> MaintainAsync(Geolocalizacao e, bool autoSave = false);
    }
}
